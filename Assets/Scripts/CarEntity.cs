using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] m_Renderders = new SpriteRenderer[5];
    public GameObject frontLineLeft;
    public GameObject frontLineRight;
    public GameObject backLine;
    public GameObject wheelFrontRight;
    public GameObject wheelFrontLeft;
    public GameObject wheelBackRight;
    public GameObject wheelBackLeft;
    Vector4 originalCarColor;
    Vector4 originalWheelColor;

    public float m_CarLength = 1;
    public float m_CarWidth = 1;
    float m_FrontWheelAngle = 0;
    string m_CarName;
    const float WHEEL_ANGLE_LIMIT = 30f;
    public float turnAngularVelocity = 20f;
    float m_TurnAngle;

    float m_Velocity;
    float m_SideVelocity;

    float m_DeltaMovement;
    float m_SideDeltaMovement;

    public float acceleration = 3f;
    public float deceleration = 10f;
    public float maxVelocity = 30f;
    [SerializeField] float decayVelocity = 0.4f;

    bool driveAssist = false;
    bool cameraZoomIn = false;

    public float CarLength { get { return m_CarLength; } }

    public float FrontWheelAngle { get { return m_FrontWheelAngle; } }

    public float Velocity { get { return m_Velocity; } }

    public float DeltaMovement { get { return m_DeltaMovement; } }

    public float AngleLimit { get { return WHEEL_ANGLE_LIMIT; } }

    public Vector4 CarColor { get { return m_Renderders[0].color; } }

    public Vector4 WheelColor { get { return m_Renderders[1].color; } }

    public Vector4 OriginalCarColor { get { return originalCarColor; } }

    public Vector4 OriginalWheelColor { get { return originalWheelColor; } }

    public bool IsDriveAssistOn { get { return driveAssist; } }

    public bool IsCameraZoomIn { get { return cameraZoomIn; } }

    public CarEntity(string carName, float carLength = 1, float frontWheelAngle = 0)
    {
        m_CarName = carName;
        m_CarLength = carLength;
        m_FrontWheelAngle = frontWheelAngle;
    }
    public void SetOriginalColor()
    {
        originalCarColor = new Color(CarColor.x, CarColor.y, CarColor.z, CarColor.w);
        originalWheelColor = new Color(WheelColor.x, WheelColor.y, WheelColor.z, WheelColor.w);
    }
    public void SpeedUp()
    {
        m_Velocity = Mathf.Min(maxVelocity, m_Velocity + Time.deltaTime * acceleration);
    }
    public void Break()
    {
        m_Velocity = Mathf.Max(0, m_Velocity - Time.deltaTime * deceleration);
    }
    public void Back()
    {
        m_Velocity = Mathf.Max(-5f, m_Velocity - Time.deltaTime * deceleration);
    }
    public void Decay()
    {
        if(m_Velocity > 0)
        {
            m_Velocity = Mathf.Max(0, m_Velocity - decayVelocity * Time.deltaTime);
        }
        else
        {
            m_Velocity = Mathf.Min(0, m_Velocity + decayVelocity * Time.deltaTime);
        }
       
        if (m_SideVelocity > 0)
        {
            m_SideVelocity = Mathf.Max(0, m_SideVelocity - 10 * decayVelocity * Time.deltaTime);
        }
        else
        {
            m_SideVelocity = Mathf.Min(0, m_SideVelocity + 10 * decayVelocity * Time.deltaTime);
        }
        m_SideDeltaMovement = m_SideDeltaMovement * Time.fixedDeltaTime;
        m_DeltaMovement = m_Velocity * Time.fixedDeltaTime;
    }
    public void Drifting(string direction)
    {
        switch (direction)
        {
            case "left":
                m_SideVelocity = 0.15f * m_Velocity;
                break;
            case "right":
                m_SideVelocity = -0.15f * m_Velocity;
                break;
            default:
                Debug.Log("direction error!");
                break;
        }
        m_Velocity = Mathf.Max(0, m_Velocity - Time.deltaTime * 0.5f * deceleration);
        m_DeltaMovement = m_Velocity * Time.fixedDeltaTime;
        m_SideDeltaMovement = m_SideVelocity * Time.fixedDeltaTime;
    }
    public void Turn(string direction)
    {
        switch (direction)
        {
            case "left":
                m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle + Time.fixedDeltaTime * turnAngularVelocity,
                -WHEEL_ANGLE_LIMIT,
                WHEEL_ANGLE_LIMIT);

                UpdateWheels();
                break;
            case "right":
                m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle - Time.fixedDeltaTime * turnAngularVelocity,
                -WHEEL_ANGLE_LIMIT,
                WHEEL_ANGLE_LIMIT);

                // Clamp is a function that controls the maximum and minimum.

                UpdateWheels();
                break;
            default:
                Debug.Log("direction error!");
                break;
        }
    }

    public void TurnReset()
    {
        if(FrontWheelAngle > 0)
        {
            m_FrontWheelAngle = Mathf.Max(
                m_FrontWheelAngle - Time.fixedDeltaTime * turnAngularVelocity, 0);
        }
        else
        {
            m_FrontWheelAngle = Mathf.Min(
                m_FrontWheelAngle + Time.fixedDeltaTime * turnAngularVelocity, 0);
        }
        UpdateWheels();
    }

    public void UpdateWheels()
    {
        // Update wheels by m_FrontWheelAngle
        float[] correctAngle = WheelAngleCorrecter();
        Vector3 localEulerAnglesLeft = new Vector3(0f, 0f, correctAngle[0]);
        Vector3 localEulerAnglesRight = new Vector3(0f, 0f, correctAngle[1]);
        wheelFrontLeft.transform.localEulerAngles = localEulerAnglesLeft;
        wheelFrontRight.transform.localEulerAngles = localEulerAnglesRight;
    }

    public float[] WheelAngleCorrecter()
    {
        float[] correctAngle = new float[2];
        float radius = m_CarLength / Mathf.Tan(Mathf.Deg2Rad * m_FrontWheelAngle);
        correctAngle[0] = Mathf.Atan(m_CarLength / (radius - m_CarWidth/2)) * Mathf.Rad2Deg;
        correctAngle[1] = Mathf.Atan(m_CarLength / (radius + m_CarWidth/2)) * Mathf.Rad2Deg;
        return correctAngle;
    }

    public void UpdatePosition()
    {
        m_TurnAngle = 1 / CarLength *
                Mathf.Tan(Mathf.Deg2Rad * FrontWheelAngle) *
                DeltaMovement *
                Mathf.Rad2Deg;

        this.transform.Rotate(0f, 0f, m_TurnAngle);

        this.transform.Translate(Vector3.up * DeltaMovement + 
            Vector3.right * m_SideDeltaMovement + 
            Vector3.right * 0.1f * Velocity * Mathf.Cos(Mathf.Deg2Rad * (90 - Mathf.Abs(FrontWheelAngle))) * m_SideDeltaMovement);
    }

    public void ResetColor()
    {
        ChangeCarColor(new Color(OriginalCarColor.x, OriginalCarColor.y, OriginalCarColor.z, OriginalCarColor.w));
        ChangeWheelColor(new Color(OriginalWheelColor.x, OriginalWheelColor.y, OriginalWheelColor.z, OriginalWheelColor.w));
    }

    void ChangeColor (Color color)
    {
        foreach (SpriteRenderer r in m_Renderders)
        {
            r.color = color;
        }
    }
    void ChangeCarColor(Color color)
    {
        m_Renderders[0].color = color;
    }
    void ChangeWheelColor(Color color)
    {
        for (int i=1; i<5; i++)
        {
            m_Renderders[i].color = color;
        }
    }

    public void ResetTranslucent()
    {
        ChangeColor(new Color(this.CarColor.x, this.CarColor.y, this.CarColor.z, 1));
    }

    public void CarTranslucent()
    {
        ChangeColor(new Color(this.CarColor.x, this.CarColor.y, this.CarColor.z, 0.7f));
    }

    public void CarChangeColor (Color carColor, Color wheelColor)
    {
        ChangeCarColor(carColor);
    }

    void  OnCollisionEnter2D(Collision2D collision)
    {
        ChangeColor(Color.red);
        Stop();
        Debug.Log("crash!");
    }
    void Stop()
    {
        m_Velocity = 0;
    }

    public void DriveAssistOn()
    {
        driveAssist = true;
    }
    public void DriveAssistOff()
    {
        driveAssist = false;
    }

    public void CameraZoomInOn()
    {
        cameraZoomIn = true;
    }

    public void CameraZoomInOff()
    {
        cameraZoomIn = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        DriveAssist m_DriveAssist = new DriveAssist();
        m_DriveAssist.Off(this);
        DriveAssistOff();
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        CameraZoomInOff();
        ResetColor();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();
        if (checkPoint != null)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 0.1f);
        }
    }
}
 
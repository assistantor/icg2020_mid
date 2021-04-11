using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorEntity : MonoBehaviour
{
    SpriteRenderer m_TargerRenderer;
    float m_WheelSteeringAngle = 0;
    void Start()
    {
        m_TargerRenderer = this.GetComponent<SpriteRenderer>();
    }

    public Vector4 GetColor { get{ return m_TargerRenderer.color; } }

    public void Press()
    {
        m_TargerRenderer.color = new Vector4 (1,1,1,GetColor.w);
    }

    public void ResetColor()
    {
        m_TargerRenderer.color = new Vector4(0.5f, 0.5f, 0.5f, GetColor.w);
    }

    public void Invisible()
    {
        m_TargerRenderer.color = new Vector4(GetColor.x, GetColor.y, GetColor.z, 0f);
    }
    public void Visible()
    {
        m_TargerRenderer.color = new Vector4(GetColor.x, GetColor.y, GetColor.z, 1f);
    }

    public void Rotation(string direction, CarEntity targetCar)
    {
        switch (direction)
        {
            case "left":
                m_WheelSteeringAngle = targetCar.FrontWheelAngle / targetCar.AngleLimit * 540;

                UpdateRotation();
                break;
            case "right":
                m_WheelSteeringAngle = targetCar.FrontWheelAngle / targetCar.AngleLimit * 540;

                UpdateRotation();
                break;
            default:
                Debug.Log("direction error!");
                break;
        }
    }
    public void WheelSteeringAngleSwitchCorrecter(CarEntity targetCar)
    {
        m_WheelSteeringAngle = targetCar.FrontWheelAngle / targetCar.AngleLimit * 540;
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        Vector3 localEulerAngles = new Vector3(0f, 0f, m_WheelSteeringAngle);
        this.transform.localEulerAngles = localEulerAngles;
    }

}
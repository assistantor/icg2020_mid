using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingCameraEntity : MonoBehaviour
{
    public CarEntity m_TargetObject;
    [SerializeField] OperatorEntity WheelSteering;
    [SerializeField] OperatorEntity Breaker;
    [SerializeField] OperatorEntity GasPedal;

    Camera m_Camera;
    float m_OrthographicSize;
    float m_Deformation;
    float m_DeformationA;

    public Vector3 fixWheelSteering;
    public Vector3 fixGasPedal;
    public Vector3 fixBreaker;

    public float MOVING_THRESHOLD = 10f;
    bool driveAssistance = false;

    // Start is called before the first frame update
    void Start()
    {
        fixWheelSteering = WheelSteering.transform.position - this.transform.position;
        fixBreaker = Breaker.transform.position - this.transform.position;
        fixGasPedal = GasPedal.transform.position - this.transform.position;

        m_Camera = this.GetComponent<Camera>();
        m_OrthographicSize = m_Camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = m_TargetObject.transform.position - this.transform.position;
        Vector2 position = deltaPos * 1f * Time.deltaTime;

        this.transform.position += new Vector3(position.x, position.y,0);
    }

    public void CameraChange(CarEntity car)
    {
        m_TargetObject = car;
    }

    private void LateUpdate()
    {   
        Vector2 deltaPos = this.transform.position - m_TargetObject.transform.position;
        // Vector3 can trans into Vector2 directly.
        // Calculate the distance reversely.
        if(deltaPos.magnitude > MOVING_THRESHOLD)
        {
            Vector2 direct = new Vector2(-1 * deltaPos.x / deltaPos.magnitude, -1 * deltaPos.y / deltaPos.magnitude);

            Vector2 newPosition = new Vector2(this.transform.position.x, this.transform.position.y)
                + direct * MOVING_THRESHOLD * 0.02f;

            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }
        /*
        else if (deltaPos.magnitude > MOVING_THRESHOLD)
        {
            deltaPos.Normalize();

            Vector2 newPosition = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y)
                + deltaPos * MOVING_THRESHOLD;

            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }
        */

        m_Deformation = m_Camera.orthographicSize / m_OrthographicSize;
        m_DeformationA = Mathf.Max(0, m_Deformation-1);

        WheelSteering.transform.localScale = new Vector3 (1+m_DeformationA, 1+m_DeformationA, 0);
        WheelSteering.transform.position = 
            new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2 (fixWheelSteering.x, fixWheelSteering.y)*m_Deformation;
        
        Breaker.transform.localScale = new Vector3 (1+m_DeformationA, 1+m_DeformationA, 0);
        Breaker.transform.position = 
            new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2 (fixBreaker.x, fixBreaker.y)*m_Deformation;

        GasPedal.transform.localScale = new Vector3 (1+m_DeformationA, 1+m_DeformationA, 0);
        GasPedal.transform.position = 
            new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2 (fixGasPedal.x, fixGasPedal.y)*m_Deformation;

        if (!(m_TargetObject.IsCameraZoomIn))
        {
            DriveAssistanceOff();
        }

        if (m_TargetObject.IsCameraZoomIn || (driveAssistance && m_TargetObject.Velocity < 5))
        {
            Invoke("DriveAssistMode", 0f);
            return;
        }
        Invoke("CameraSize", 0.3f);
    }

    void CameraSize()
    {
        m_Camera.orthographicSize = m_OrthographicSize + Mathf.Max(0, Mathf.Abs(m_TargetObject.Velocity)) * 0.35f;
        WheelSteering.Visible();
        Breaker.Visible();
        GasPedal.Visible();
    }

    public void DriveAssistanceOn()
    {
        if(m_TargetObject.Velocity < 5)
        {
            this.transform.position = new Vector3(m_TargetObject.transform.position.x, m_TargetObject.transform.position.y, this.transform.position.z);
        }
        driveAssistance = true;
        CameraZoomIn();
    }
    public void DriveAssistanceOff()
    {
        driveAssistance = false;
        CameraZoomOut();
        CameraSize();
    }
    public void DriveAssistMode()
    {
        m_Camera.orthographicSize = 5.5f;
        WheelSteering.Invisible();
        Breaker.Invisible();
        GasPedal.Invisible();
    }
    public void CameraZoomIn()
    {
        m_TargetObject.CameraZoomInOn();
    }
    public void CameraZoomOut()
    {
        m_TargetObject.CameraZoomInOff();
    }
}

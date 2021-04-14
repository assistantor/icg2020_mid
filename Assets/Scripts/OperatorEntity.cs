using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorEntity : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_TargerRenderer;
    float m_WheelSteeringAngle = 0;

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

    public void Rotation(CarEntity targetCar)
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
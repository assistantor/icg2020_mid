using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveAssist
{
    float angle;
    public void On(CarEntity m_car)
    {
        m_car.frontLineLeft.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        m_car.frontLineRight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        m_car.backLine.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        m_car.CarTranslucent();
    }

    public void Off(CarEntity m_car)
    {
        m_car.frontLineLeft.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        m_car.frontLineRight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        m_car.backLine.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        m_car.ResetTranslucent();
    }

    public void Turn(CarEntity m_car)
    {
        angle = m_car.FrontWheelAngle;
        float f = -angle / m_car.AngleLimit;
        if(angle > 0)
        {
            m_car.frontLineRight.transform.localPosition = new Vector3(2 * f , 0, m_car.frontLineRight.transform.localPosition.z);
            m_car.frontLineLeft.transform.localPosition  = new Vector3(f , 0, m_car.frontLineLeft.transform.localPosition.z);
        }
        else
        {
            m_car.frontLineRight.transform.localPosition = new Vector3(f , 0, m_car.frontLineRight.transform.localPosition.z);
            m_car.frontLineLeft.transform.localPosition  = new Vector3(2 * f, 0, m_car.frontLineLeft.transform.localPosition.z);
        }
        m_car.backLine.transform.localPosition = new Vector3(f , -0.5f, m_car.frontLineLeft.transform.localPosition.z);
    }
}

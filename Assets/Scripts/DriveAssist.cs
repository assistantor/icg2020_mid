using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveAssist
{
    float angle;
    public void On(CarEntity m_car)
    {
        m_car.frontLineLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        m_car.frontLineRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        m_car.backLine.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        m_car.CarTranslucent();
    }

    public void Off(CarEntity m_car)
    {
        m_car.frontLineLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        m_car.frontLineRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        m_car.backLine.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        m_car.ResetTranslucent();
    }

    public void Turn(CarEntity m_car)
    {
        angle = m_car.FrontWheelAngle;
        float f = angle / m_car.AngleLimit;
        float[] correctAngle = m_car.WheelAngleCorrecter();
        Vector3 frontLineLeftCenter = new Vector3(-0.5f, 0.5f, m_car.frontLineLeft.transform.localPosition.z);
        Vector3 frontLineRightCenter = new Vector3(0.5f, 0.5f, m_car.frontLineLeft.transform.localPosition.z);
        Vector3 backLineCenter = new Vector3(0, -0.5f, m_car.frontLineLeft.transform.localPosition.z);
        m_car.frontLineLeft.transform.localEulerAngles = new Vector3(0, 0, correctAngle[0]);
        m_car.frontLineRight.transform.localEulerAngles = new Vector3(0, 0, correctAngle[1]);
        if(angle > 0)
        {
            m_car.frontLineRight.transform.localPosition =
            frontLineRightCenter -
            new Vector3(2*f * Mathf.Cos(Mathf.Deg2Rad * correctAngle[1]), 2*f * Mathf.Sin(Mathf.Deg2Rad * correctAngle[1]), 0);
            m_car.frontLineLeft.transform.localPosition =
            frontLineLeftCenter -
            new Vector3(f * Mathf.Cos(Mathf.Deg2Rad * correctAngle[0]), f * Mathf.Sin(Mathf.Deg2Rad * correctAngle[0]), 0);

        }
        else
        {
            m_car.frontLineRight.transform.localPosition =
            frontLineRightCenter -
            new Vector3(f * Mathf.Cos(Mathf.Deg2Rad * correctAngle[1]), f * Mathf.Sin(Mathf.Deg2Rad * correctAngle[1]), 0);
            m_car.frontLineLeft.transform.localPosition =
            frontLineLeftCenter -
            new Vector3(2 * f * Mathf.Cos(Mathf.Deg2Rad * correctAngle[0]), 2 * f * Mathf.Sin(Mathf.Deg2Rad * correctAngle[0]), 0);

        }
        m_car.backLine.transform.localPosition = 
            backLineCenter -
            new Vector3(f , 0, 0);
    }
}

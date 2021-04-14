using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingPos : MonoBehaviour
{
    CarEntity m_car;
    [SerializeField] GameObject center;
    [SerializeField] GameObject line1;
    [SerializeField] GameObject line2;
    [SerializeField] GameObject line3;
    [SerializeField] GameObject line4;

    void OnTriggerStay2D(Collider2D other)
    {
        m_car = other.gameObject.GetComponent<CarEntity>();
        if (m_car != null)
        {
            Vector4 carColor = m_car.OriginalCarColor;
            Vector4 wheelColor = m_car.OriginalWheelColor;
            if (center.GetComponent<CheckLine>().IsPassing &&
            !(line1.GetComponent<CheckLine>().IsPassing) &&
            !(line2.GetComponent<CheckLine>().IsPassing) &&
            !(line3.GetComponent<CheckLine>().IsPassing) &&
            !(line4.GetComponent<CheckLine>().IsPassing))
            {
                Debug.Log("nice job.");
                m_car.CarChangeColor(new Color(0,1,0,m_car.CarColor.w), 
                    new Color(0, 1, 0, m_car.WheelColor.w));
            }
            else
            {
                Debug.Log("you're out");

                m_car.CarChangeColor(new Color(carColor.x, carColor.y, carColor.z, m_car.CarColor.w), 
                    new Color(wheelColor.x, wheelColor.y, wheelColor.z, m_car.WheelColor.w));
            }
        }
    }

}
    

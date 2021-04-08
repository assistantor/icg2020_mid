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
            if (center.GetComponent<CheckLine>().IsPassing &&
            !(line1.GetComponent<CheckLine>().IsPassing) &&
            !(line2.GetComponent<CheckLine>().IsPassing) &&
            !(line3.GetComponent<CheckLine>().IsPassing) &&
            !(line4.GetComponent<CheckLine>().IsPassing))
            {
                Debug.Log("nice job.");
                m_car.CarChangeColor(Color.green);
            }
            else
            {
                Debug.Log("you're out");
                m_car.CarResetColor();
            }
        }
    }

}
    

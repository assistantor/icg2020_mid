using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLine : MonoBehaviour
{
    CarEntity m_car;
    bool isPassing = false;

    public bool IsPassing { get { return isPassing; } }

    void OnTriggerExit2D(Collider2D other)
    {
        m_car = other.gameObject.GetComponent<CarEntity>();
        if (m_car != null)
        {
            PassedLine();
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        m_car = other.gameObject.GetComponent<CarEntity>();
        if (m_car != null)
        {
            PassingLine();
        }
    }

    
    public void PassingLine()
    {
        isPassing = true;
    }

    public void PassedLine()
    {
        isPassing = false;
    }

}

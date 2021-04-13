using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGames : MonoBehaviour
{
    CarControl m_Game;

    // Start is called before the first frame update
    void Start()
    {
        m_Game = new CarControl();
        m_Game.SetGame();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Game.Decay();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Break
            m_Game.Break();
        }
        else if (Input.GetKey(KeyCode.B))
        {
            // reset Breaker
            m_Game.BreakerReset();
            //Back
            m_Game.Back();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftArrow))
            {
                m_Game.Drifting("left");
            }else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.RightArrow))
            {
                m_Game.Drifting("right");
            }
            else
            {
                // reset Breaker
                m_Game.BreakerReset();
                // Speed up
                m_Game.SpeedUp();
            }
        }
        else
        {
            // reset GasPedalReset
            m_Game.GasPedalReset();
            // reset Breaker
            m_Game.BreakerReset();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.B))
        {
            // reset breaker
            m_Game.GasPedalReset();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Back
            m_Game.BreakerReset();
        }

        if ((Input.GetKey(KeyCode.X) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            // Turn reset
            m_Game.TurnReset();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Turn left
            m_Game.Turn("left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Turn right
            m_Game.Turn("right");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Game.SelectCar();
        }
        // Update car transform
        m_Game.UpdatePosition();
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_Game.DriveAssistance();
        }

        
    }
}

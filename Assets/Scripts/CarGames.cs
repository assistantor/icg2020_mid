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
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Break
            m_Game.Break();
        }
        else if (Input.GetKey(KeyCode.B))
        {
            //Back
            m_Game.Back();
            // reset Breaker
            m_Game.BreakerReset();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            // Speed up
            m_Game.SpeedUp();
            // reset Breaker
            m_Game.BreakerReset();
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

        if (Input.GetKey(KeyCode.LeftArrow))
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
        
        

    }
}

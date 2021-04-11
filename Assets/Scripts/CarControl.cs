using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl
{
    public List<CarEntity> m_Cars = new List<CarEntity>();      
    public int m_CarSelectIndex = 0;                            //定義car list & 當前汽車編號
    public CarEntity m_SelectCar;
    public CarEntity Car1;
    public CarEntity Car2;
    public TracingCameraEntity m_Camera;

    

    public OperatorEntity m_WheelSteering;
    public OperatorEntity m_Breaker;
    public OperatorEntity m_GasPedal;

    DriveAssist m_DriveAssist = new DriveAssist();
    bool assistControl = false;

    public void SetGame()
    {
        Car1 = GameObject.Find("car1").GetComponent<CarEntity>();
        Car2 = GameObject.Find("car2").GetComponent<CarEntity>();
        Car1.SetOriginalColor();
        Car2.SetOriginalColor();

        m_WheelSteering = GameObject.Find("steeringwheel").GetComponent<OperatorEntity>();
        m_Breaker = GameObject.Find("breaker").GetComponent<OperatorEntity>();
        m_GasPedal = GameObject.Find("gaspedal").GetComponent<OperatorEntity>();

        m_Camera = GameObject.Find("Main Camera").GetComponent<TracingCameraEntity>();
        m_Camera.targetObject = Car1;

        //找到hierarchy中的車子放入List & 鏡頭功能並鎖定車一

        m_Cars.Add(Car1);
        m_Cars.Add(Car2);
        m_SelectCar = m_Cars[0];
    }

    public void SpeedUp()
    {
        m_SelectCar.SpeedUp();
        m_GasPedal.Press();
    }
    public void Back()
    {
        m_SelectCar.Back();
        m_GasPedal.Press();
    }

    public void GasPedalReset()
    {
        m_GasPedal.ResetColor();
    }

    public void Break()
    {
        m_SelectCar.Break();
        m_Breaker.Press();
    }

    public void BreakerReset()
    {
        m_Breaker.ResetColor();
    }

    public void Turn(string direction)
    {
        m_SelectCar.Turn(direction);
        m_WheelSteering.Rotation(direction, m_SelectCar);
        m_DriveAssist.Turn(m_SelectCar);

    }

    public void Drifting(string direction)
    {
        m_SelectCar.Drifting(direction);
        m_Breaker.Press();
        m_GasPedal.Press();
    }

    public void SelectCar()
    {
        if (assistControl)
        {
            DriveAssistance();
        }
        if (++m_CarSelectIndex >= m_Cars.Count)
        {
            m_CarSelectIndex = 0;
        }

        m_SelectCar = m_Cars[m_CarSelectIndex];
        m_WheelSteering.WheelSteeringAngleSwitchCorrecter(m_SelectCar);
        m_Camera.CameraChange(m_SelectCar);
    }

    public void DriveAssistance()
    {
        if (!assistControl)
        {
            assistControl = true;
            m_DriveAssist.On(m_SelectCar);
            m_Camera.DriveAssistanceOn();
        }
        else
        {
            assistControl = false;
            m_DriveAssist.Off(m_SelectCar);
            m_Camera.DriveAssistanceOff();
        }
    }

    public void Decay()
    {
        foreach (CarEntity car in m_Cars)
        {
            car.Decay();
        }
    }

    public void UpdatePosition()
    {
        foreach(CarEntity car in m_Cars)
        {
            car.UpdatePosition();
        }
    }
}

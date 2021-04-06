using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl
{
    public List<CarEntity> m_Cars = new List<CarEntity>();
    public int m_CarSelectIndex = 0;
    public CarEntity m_SelectCar;
    public CarEntity Car1;
    public CarEntity Car2;
    public TracingCameraEntity m_Camera;



    public void SetGame()
    {
        Car1 = GameObject.Find("car1").GetComponent<CarEntity>();
        Car2 = GameObject.Find("car2").GetComponent<CarEntity>();

        m_Camera = GameObject.Find("Main Camera").GetComponent<TracingCameraEntity>();
        m_Camera.targetObject = Car1;

        m_Cars.Add(Car1);
        m_Cars.Add(Car2);
        m_SelectCar = m_Cars[0];
    }

    public void SpeedUp()
    {
        m_SelectCar.SpeedUp();
    }
    public void Back()
    {
        m_SelectCar.Back();
    }
    public void Break()
    {
        m_SelectCar.Break();
    }
    public void Turn(string direction)
    {
        m_SelectCar.Turn(direction);
    }
    public void SelectCar()
    {
        if (++m_CarSelectIndex >= m_Cars.Count)
        {
            m_CarSelectIndex = 0;
        }

        m_SelectCar = m_Cars[m_CarSelectIndex];
        m_Camera.CameraChange(m_SelectCar);
    }


    public void UpdatePosition()
    {
        foreach(CarEntity car in m_Cars)
        {
            car.transform.Rotate(0f, 0f, 1 / car.CarLength *
            Mathf.Tan(Mathf.Deg2Rad * car.FrontWheelAngle) *
            car.DeltaMovement *
            Mathf.Rad2Deg);

            car.transform.Translate(Vector3.up * car.DeltaMovement);
        }
    }
}

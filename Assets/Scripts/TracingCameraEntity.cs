using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingCameraEntity : MonoBehaviour
{
    public CarEntity targetObject;

    Camera m_Camera;
    float m_OrthographicSize;

    public float MOVING_THRESHOLD = 10f;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = this.GetComponent<Camera>();
        m_OrthographicSize = m_Camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPos = targetObject.transform.position - this.transform.position;
        Vector3 position = deltaPos * 1f * Time.deltaTime;

        this.transform.position += new Vector3(position.x, position.y, 0);
    }

    public void CameraChange(CarEntity car)
    {
        targetObject = car;

    }

    private void LateUpdate()
    {
        Vector2 deltaPos = this.transform.position - targetObject.transform.position;
        // Vector3 can trans into Vector2 directly.
        // Calculate the distance reversely.
        if(deltaPos.magnitude > MOVING_THRESHOLD)
        {
            Vector2 direct = new Vector2(-1 * deltaPos.x / deltaPos.magnitude, -1 * deltaPos.y / deltaPos.magnitude);

            Vector2 newPosition = new Vector2(this.transform.position.x, this.transform.position.y)
                + direct * MOVING_THRESHOLD * 0.02f;

            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }
        /*
        else if (deltaPos.magnitude > MOVING_THRESHOLD)
        {
            deltaPos.Normalize();

            Vector2 newPosition = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y)
                + deltaPos * MOVING_THRESHOLD;

            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }
        */

        Invoke("CameraSize", 0.5f);
    }
    void CameraSize()
    {
        m_Camera.orthographicSize = m_OrthographicSize + Mathf.Max(0, targetObject.Velocity) * 0.4f;
    }
}

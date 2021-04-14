using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        CarEntity car = other.gameObject.GetComponent<CarEntity>();
        if (car != null)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

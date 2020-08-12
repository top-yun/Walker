using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseScript : MonoBehaviour
{
    public float x, y;

    public float speed;

    private float temp;
    // Update is called once per frame
    void FixedUpdate()
    {
        temp += speed;
        this.transform.localPosition = new Vector3(x * Mathf.Cos(temp), y * Mathf.Sin(temp));
        
    }
}

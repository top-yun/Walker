using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlanet : MonoBehaviour
{
    public float round;

    private float speed;
    private float time;
    void Awake()
    {
        speed = 0.01f * (1 / Mathf.Sqrt(Mathf.Pow(round, 3)));
        time = Random.Range(0, 360);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += speed;
        this.transform.position = new Vector2(round * Mathf.Cos(time), round * Mathf.Sin(time));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform backGround;
    public float movement;
    public Vector2 startPoint;
    // Start is called before the first frame update

    private Vector2 offset;


    // Update is called once per frame
    void FixedUpdate()
    {
        offset = (Vector2)(this.transform.position) - startPoint / movement;
        backGround.position = (Vector2)this.transform.position - offset * movement;
    }
}

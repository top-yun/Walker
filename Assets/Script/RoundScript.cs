using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 angle = this.transform.eulerAngles;

        angle.z += speed;

        this.transform.eulerAngles = angle;
    }
}

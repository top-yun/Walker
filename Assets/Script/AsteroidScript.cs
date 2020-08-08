using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 angle = this.transform.eulerAngles;

        angle.z += 0.1f;

        this.transform.eulerAngles = angle;

    }
}

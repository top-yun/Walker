using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(0,0.005f);

        if (this.transform.position.y > 15f)
        {
            this.transform.position = new Vector2(0, -15f);
        }

    }
}

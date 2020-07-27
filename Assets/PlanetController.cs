using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject Blackhole;
    public bool OnBlackhole;
    public GameObject Whitehole;
    public bool OnWhitehole;
    public float maxRange;

    private GameObject hasBlackhole;
    private GameObject hasWhitehole;

    // Start is called before the first frame update
    void Start()
    {
        hasBlackhole = null;
        hasWhitehole = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && hasBlackhole == null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);

            if (maxRange > Vector3.Distance(rayDir, this.transform.position))
            {
                hasBlackhole = Instantiate(Blackhole, rayDir, transform.rotation);
            }
        }

        if (Input.GetKey(KeyCode.Alpha2) && hasWhitehole == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);

            if (maxRange > Vector3.Distance(rayDir, this.transform.position))
            {
                hasWhitehole = Instantiate(Whitehole, rayDir, transform.rotation);
            }
        }
    }
}

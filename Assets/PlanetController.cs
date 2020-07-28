using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject Blackhole;
    public bool OnBlackhole;
    public GameObject Whitehole;
    public bool OnWhitehole;
    public GameObject boundary;
    public float maxRange;
    public float timeDelay;

    enum hole
    {
        blackhole,
        whitehole
    }

    private GameObject hasBlackhole;
    private GameObject hasWhitehole;
    private GameObject hasBoundary;
    private bool isblockcool;
    private bool iswhitecool;
    private WaitForSeconds Cooltime;

    // Start is called before the first frame update
    void Start()
    {
        hasBlackhole = null;
        hasWhitehole = null;
        isblockcool = true;
        iswhitecool = true;

        Cooltime = new WaitForSeconds(2f);
    }

    IEnumerator cooltime(hole h)
    {
        if (h == hole.blackhole)
            isblockcool = false;
        else if (h == hole.whitehole)
            iswhitecool = false;
        yield return Cooltime;
        if (h == hole.blackhole)
            isblockcool = true;
        else if (h == hole.whitehole)
            iswhitecool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (hasBlackhole == null && Time.timeScale == 1 && isblockcool)
            {
                Time.timeScale = timeDelay;
                if (hasBoundary == null)
                {
                    hasBoundary = Instantiate(boundary, this.transform);
                    hasBoundary.transform.localScale = new Vector2(0.78f * maxRange, 0.78f * maxRange);
                }
            } else
            {
                Destroy(hasBlackhole);
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && Time.timeScale == timeDelay)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);

            if (maxRange > Vector3.Distance(rayDir, this.transform.position))
            {
                hasBlackhole = Instantiate(Blackhole, rayDir, transform.rotation);
            }
            Time.timeScale = 1;
            Destroy(hasBoundary);
            StartCoroutine(cooltime(hole.blackhole));
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasWhitehole == null && Time.timeScale == 1 && iswhitecool)
            {
                Time.timeScale = timeDelay;
                if (hasBoundary == null)
                {
                    hasBoundary = Instantiate(boundary, this.transform);
                    hasBoundary.transform.localScale = new Vector2(0.78f * maxRange, 0.78f * maxRange);
                }
            } else
            {
                Destroy(hasWhitehole);
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && Time.timeScale == timeDelay)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);

            if (maxRange > Vector3.Distance(rayDir, this.transform.position))
            {
                hasWhitehole = Instantiate(Whitehole, rayDir, transform.rotation);
            }
            Time.timeScale = 1;
            Destroy(hasBoundary);
            StartCoroutine(cooltime(hole.whitehole));
        }
    }
}

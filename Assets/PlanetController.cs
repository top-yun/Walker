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
        None,
        blackhole,
        whitehole
    }

    private GameObject hasBlackhole;
    private GameObject hasWhitehole;
    private GameObject hasBoundary;
    private bool isblockcool;
    private bool iswhitecool;
    private WaitForSeconds Cooltime;
    private hole hasClicked;
    private Coroutine delayCorou;

    // Start is called before the first frame update
    void Start()
    {
        resetAll();

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

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        Destroy(hasBoundary);
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
                    hasBoundary.transform.localScale = new Vector2( 4* maxRange, 4* maxRange);
                }
                hasClicked = hole.blackhole;
                delayCorou = StartCoroutine(Delay());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasWhitehole == null && Time.timeScale == 1 && iswhitecool)
            {
                Time.timeScale = timeDelay;
                if (hasBoundary == null)
                {
                    hasBoundary = Instantiate(boundary, this.transform);
                    hasBoundary.transform.localScale = new Vector2(4 * maxRange, 4 * maxRange);
                }
                hasClicked = hole.whitehole;
                delayCorou = StartCoroutine(Delay());
            }
        }

        if (Input.GetMouseButton(0) && Time.timeScale == timeDelay)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);

            if (maxRange > Vector3.Distance(rayDir, this.transform.position))
            {
                if (hasClicked == hole.blackhole)
                    hasBlackhole = Instantiate(Blackhole, rayDir, transform.rotation);
                else if (hasClicked == hole.whitehole)
                    hasWhitehole = Instantiate(Whitehole, rayDir, transform.rotation);
            }
            StopCoroutine(delayCorou);
            Time.timeScale = 1;
            Destroy(hasBoundary);
            if (hasClicked == hole.blackhole)
                StartCoroutine(cooltime(hole.blackhole));
            else if (hasClicked == hole.whitehole)
                StartCoroutine(cooltime(hole.whitehole));
        }
    }

    public void resetAll()
    {
        Destroy(hasBlackhole);
        Destroy(hasWhitehole);

        isblockcool = true;
        iswhitecool = true;

        hasClicked = hole.None;
    }
}

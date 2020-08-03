using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    public GameObject Blackhole;
    public bool OnBlackhole;
    public GameObject Whitehole;
    public bool OnWhitehole;
    public GameObject boundary;
    public float maxRange;
    public float minRange;
    public float timeDelay;

    public Slider blackCooltime;
    public Slider whiteCooltime;

    enum hole
    {
        None,
        blackhole,
        whitehole
    }

    private GameObject hasBlackhole;
    private GameObject hasWhitehole;
    private GameObject hasMaxBoundary;
    private GameObject hasMinBoundary;
    private bool isblockcool;
    private bool iswhitecool;
    private hole hasClicked;
    private Coroutine delayCorou;

    // Start is called before the first frame update
    void Start()
    {
        resetAll();

    }

    IEnumerator cooltime(hole h)
    {
        if (h == hole.blackhole)
        {
            isblockcool = false;
            blackCooltime.value = 0;
            while (blackCooltime.value != 2)
            {
                blackCooltime.value = Mathf.Clamp(blackCooltime.value + Time.smoothDeltaTime, 0 , 2);
                yield return null;
            }
         
            isblockcool = true;
            
        }
        else if (h == hole.whitehole)
        {
            iswhitecool = false;
            whiteCooltime.value = 0;
            while (whiteCooltime.value != 2)
            {
                whiteCooltime.value = Mathf.Clamp(whiteCooltime.value + Time.smoothDeltaTime, 0, 2);
                yield return null;
            }
            iswhitecool = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        Destroy(hasMaxBoundary);
        Destroy(hasMinBoundary);
        StartCoroutine(cooltime(hole.blackhole));
        StartCoroutine(cooltime(hole.whitehole));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (hasBlackhole == null && Time.timeScale == 1 && isblockcool)
            {
                Time.timeScale = timeDelay;
                if (hasMaxBoundary == null)
                {
                    hasMaxBoundary = Instantiate(boundary, this.transform);
                    // hasMinBoundary = Instantiate(boundary, this.transform);

                    hasMaxBoundary.transform.localScale = new Vector2( 4* maxRange, 4* maxRange);
                    // hasMinBoundary.transform.localScale = new Vector2(4 * minRange, 4 * minRange);
                }
                hasClicked = hole.blackhole;
                delayCorou = StartCoroutine(Delay());
            } else if (hasBlackhole != null)
            {
                Destroy(hasBlackhole);
                StartCoroutine(cooltime(hole.blackhole));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasWhitehole == null && Time.timeScale == 1 && iswhitecool)
            {
                Time.timeScale = timeDelay;
                if (hasMaxBoundary == null)
                {
                    hasMaxBoundary = Instantiate(boundary, this.transform);
                    hasMinBoundary = Instantiate(boundary, this.transform);

                    hasMaxBoundary.transform.localScale = new Vector2(4 * maxRange, 4 * maxRange);
                    hasMinBoundary.transform.localScale = new Vector2(4 * minRange, 4 * minRange);
                }
                hasClicked = hole.whitehole;
                delayCorou = StartCoroutine(Delay());
            } else if (hasWhitehole != null)
            {
                Destroy(hasWhitehole);
                StartCoroutine(cooltime(hole.whitehole));
            }
        }

        if (Input.GetMouseButton(0) && Time.timeScale == timeDelay)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayDir = new Vector3(ray.origin.x, ray.origin.y);
            float distance = Vector3.Distance(rayDir, this.transform.position);


            if (hasClicked == hole.blackhole && maxRange > distance)
                hasBlackhole = Instantiate(Blackhole, rayDir, transform.rotation);
            else if (hasClicked == hole.whitehole && maxRange > distance && minRange < distance)
                hasWhitehole = Instantiate(Whitehole, rayDir, transform.rotation);
            
            StopCoroutine(delayCorou);
            Time.timeScale = 1;
            Destroy(hasMaxBoundary);
            Destroy(hasMinBoundary);
        }
    }

    public void resetAll()
    {
        Destroy(hasBlackhole);
        Destroy(hasWhitehole);

        isblockcool = true;
        iswhitecool = true;

        blackCooltime.value = 2;
        whiteCooltime.value = 2;

        Time.timeScale = 1f;

        hasClicked = hole.None;
    }
}

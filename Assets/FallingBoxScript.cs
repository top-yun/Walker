using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoxScript : MonoBehaviour
{
    public float time;

    private Renderer krenderer;
    private WaitForSeconds falldown;
    private bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        falldown = new WaitForSeconds(time/20);
        krenderer = this.GetComponent<Renderer>();
        isStart = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStart)
        {
            foreach (ContactPoint2D cp in collision.contacts)
            {
                Debug.Log((Vector2.Dot(cp.normal, Vector2.up)));
                if (Vector2.Dot(cp.normal, Vector2.down) > 0.5)
                {
                    isStart = false;
                    StartCoroutine(falling());
                    return;
                }
            }
        }
    }

    IEnumerator falling()
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            Color c = krenderer.material.color;
            c.a = f;
            krenderer.material.color = c;
            yield return falldown;
        }
        Destroy(this.gameObject);
    }
    // Update is called once per frame

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoxScript : MonoBehaviour
{
    public float time;
    public float fallAndRespone;

    private Renderer krenderer;
    private WaitForSeconds falldown;
    private bool isStart;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        falldown = new WaitForSeconds(time/20);
        krenderer = this.GetComponent<Renderer>();
        isStart = true;
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStart)
        {
            foreach (ContactPoint2D cp in collision.contacts)
            {
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
        for (float f = 1f; f > 0.1f; f -= 0.05f)
        {
            Color c = krenderer.material.color;
            c.a = f;
            krenderer.material.color = c;
            yield return falldown;
        }

        if (fallAndRespone != 0)
        {
            StartCoroutine(making());
        } else
        {
            Destroy(this.gameObject);
        }
        
    }

    IEnumerator making()
    {
        boxCollider2D.isTrigger = true;
        yield return new WaitForSeconds(fallAndRespone);
        boxCollider2D.isTrigger = false;

        Color c = krenderer.material.color;
        c.a = 1f;
        krenderer.material.color = c;

        isStart = true;
    }
    // Update is called once per frame

}

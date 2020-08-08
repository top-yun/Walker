using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Control
    {
        Remove,
        RemoveAndMake,
        Make,
        MakeAndRemove
    }
    public Control switchCase;

    public GameObject[] Object;
    public float Alpha;

    private Renderer[] objRenderer;
    private Collider2D[] objBox;
    private Coroutine Corout;
    private int objNums;

    private bool isStart;
    private int touchingCount;

    private void Awake()
    {
        isStart = true;
        touchingCount = 0;

        objNums = Object.Length;

        objRenderer = new Renderer[objNums];
        objBox = new Collider2D[objNums];

        for (int i = 0; i < objNums; i++)
        {
            objRenderer[i] = Object[i].GetComponent<Renderer>();
            objBox[i] = Object[i].GetComponent<Collider2D>();
        }
        switchSetting();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingCount++;
        if (isStart)
        {
            isStart = false;

            Corout = StartCoroutine(switchOn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingCount--;
        if (touchingCount == 0)
        {
            switchOff();
        }
    }

    IEnumerator switchOn()
    {
        switch (switchCase)
        {
            case Control.Remove:
                for (float f = 1f; f > 0; f -= 0.05f)
                {
                    for (int i = 0; i < objNums; i++)
                    {
                        Color c = objRenderer[i].material.color;
                        c.a = f;
                        objRenderer[i].material.color = c;
                    }
                    yield return null;
                }
                for (int i = 0; i< objNums; i++)
                {
                    Destroy(Object[i]);
                }
                break;
            case Control.RemoveAndMake:
                for (float f = 1f; f > Alpha; f -= 0.05f)
                {
                    for (int i = 0; i < objNums; i++)
                    {
                        Color c = objRenderer[i].material.color;
                        c.a = f;
                        objRenderer[i].material.color = c;
                    }
                    yield return null;
                }
                for (int i = 0; i < objNums; i++)
                {
                    objBox[i].isTrigger = true;
                }
                break;
            case Control.Make:
            case Control.MakeAndRemove:
                for (float f = Alpha; f < 1f; f += 0.05f)
                {
                    for (int i = 0; i < objNums; i++)
                    {
                        Color c = objRenderer[i].material.color;
                        c.a = f;
                        objRenderer[i].material.color = c;
                    }
                    yield return null;
                }

                for (int i = 0; i < objNums; i++)
                {
                    Color c = objRenderer[i].material.color;
                    c.a = 1f;
                    objRenderer[i].material.color = c;
                    objBox[i].isTrigger = false;
                }

                break;
        }
    }
    

    void switchOff()
    {
        switch (switchCase)
        {
            case Control.Remove:
            case Control.Make:
                break;
            case Control.RemoveAndMake:
                StopCoroutine(Corout);

                for (int i = 0; i < objNums; i++)
                {
                    Color c = objRenderer[i].material.color;
                    c.a = 1f;
                    objRenderer[i].material.color = c;
                    objBox[i].isTrigger = false;
                }
                isStart = true;
                break;
            case Control.MakeAndRemove:
                StopCoroutine(Corout);

                for (int i = 0; i < objNums; i++)
                {
                    Color c = objRenderer[i].material.color;
                    c.a = Alpha;
                    objRenderer[i].material.color = c;
                    objBox[i].isTrigger = true;
                }
                isStart = true;
                break;

        }
    }

    void switchSetting()
    {
        switch (switchCase)
        {
            case Control.Make:
            case Control.MakeAndRemove:
                for (int i = 0; i < objNums; i++)
                {
                    Color c = objRenderer[i].material.color;
                    c.a = Alpha;
                    objRenderer[i].material.color = c;

                    objBox[i].isTrigger = true;
                }
                break;
            default:
                for (int i = 0; i < objNums; i++)
                {
                    objBox[i].isTrigger = false;
                }
                break;
        }
    }
}

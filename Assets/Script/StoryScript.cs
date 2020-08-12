using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    public bool isAble;
    public int level;
    public GameObject story;

    private void OnMouseEnter()
    {
        if (isAble)
            story.SetActive(true);
    }
    private void OnMouseExit()
    {
        story.SetActive(false);
    }

    public static void SetStory()
    {
        StoryScript[] objsScript = GameObject.FindObjectsOfType<StoryScript>();

        int playerLevel = PlayerPrefs.GetInt("level");

        foreach (var scr in objsScript)
        {
            Renderer renderer = scr.gameObject.GetComponent<Renderer>();
            Color color = renderer.material.color;
            if (scr.level <= playerLevel)
            {
                scr.isAble = true;
                color.a = 1f;
                renderer.material.color = color;
            }else
            {
                scr.isAble = false;
                color.a = 0.2f;
                renderer.material.color = color;
            }
        }


    }
}

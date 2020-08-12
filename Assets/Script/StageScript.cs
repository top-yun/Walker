using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScript : MonoBehaviour
{
    public int level;
    public bool isAble;
    public string stageScene;
    public void OnMouseDown()
    {
        if (this.isAble)
        {
            SceneManager.LoadScene(stageScene);
        }
    }

    public static void SetStage()
    {
        StageScript[] objsScript = GameObject.FindObjectsOfType<StageScript>();
        int playerLevel = PlayerPrefs.GetInt("level");
        Debug.Log("this level is : " + playerLevel);

        foreach (var scr in objsScript)
        {
            Renderer renderer = scr.gameObject.GetComponent<Renderer>();
            Color color = renderer.material.color;
            if (scr.level <= playerLevel)
            {
                scr.isAble = true;
                color.a = 1f;
                renderer.material.color = color;
            } else if (scr.gameObject.tag == "BonusMap")
            {
                scr.isAble = false;
                color.a = 0f;
                renderer.material.color = color;
            } else
            {
                scr.isAble = false;
                color.a = 0.2f;
                renderer.material.color = color;
            }
        }


    }
}

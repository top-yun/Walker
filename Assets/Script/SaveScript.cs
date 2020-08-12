using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerPrefs.HasKey("level"))
        {

        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
        }
    }

    public static void Clear(int clearLevel)
    {
        if (PlayerPrefs.GetInt("level") <= clearLevel)
        {
            PlayerPrefs.SetInt("level", clearLevel+1);
        } 
    }
}

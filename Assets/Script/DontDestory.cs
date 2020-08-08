using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    public string tagStr;
    // Start is called before the first frame update
    private void Awake()
    {
        var backgroundMusics = GameObject.FindGameObjectsWithTag(tagStr);
        if (backgroundMusics.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageClearScript : MonoBehaviour
{
    public int level;
    public string nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SaveScript.Clear(level);

            SceneManager.LoadScene(nextLevel);
        }
    }
}

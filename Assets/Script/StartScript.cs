using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }
    public void SetListScene()
    {
        // SceneManager.LoadScene("DemoScene");
        Debug.Log("what");
        StartCoroutine(MoveCamera(new Vector2(21, 0)));
    }

    IEnumerator MoveCamera(Vector2 movement)
    {
        Transform cameraTrans = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        Vector3 tempTrans = new Vector2(0.01f * (movement.x - cameraTrans.position.x), 0.01f * (movement.y - cameraTrans.position.y));
        Debug.Log(tempTrans);
        for (int i = 0; i <= 100; i++)
        {
            cameraTrans.position += tempTrans;
            //Debug.Log(cameraTrans.position);
            yield return null;
        }
    }
}

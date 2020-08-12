using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GameObject title;
    public GameObject mainMenu;
    public GameObject rightButton;
    public GameObject leftButton;

    public void SetListScene(int x)
    {
        title.SetActive(false);
        mainMenu.SetActive(false);
        rightButton.SetActive(false);
        leftButton.SetActive(false);

        StageScript.SetStage();
        StoryScript.SetStory();
        StartCoroutine(MoveCamera(new Vector2(x, 0)));
    }

    IEnumerator MoveCamera(Vector2 movement)
    {

        Transform cameraTrans = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        Vector3 tempTrans = new Vector2(0.02f * movement.x, 0.02f * movement.y);
        Debug.Log(tempTrans);
        for (int i = 0; i <= 50; i++)
        {
            cameraTrans.position += tempTrans;
            //Debug.Log(cameraTrans.position);
            yield return null;
        }
        if (cameraTrans.position.x < 1)
        {
            title.SetActive(true);
            mainMenu.SetActive(true);
            rightButton.SetActive(false);
            leftButton.SetActive(false);
        } else if (cameraTrans.position.x > 83)
        {
            title.SetActive(false);
            mainMenu.SetActive(false);
            rightButton.SetActive(false);
            leftButton.SetActive(true);
        }        
        else
        {
            title.SetActive(false);
            mainMenu.SetActive(false);
            rightButton.SetActive(true);
            leftButton.SetActive(true);
        }
    }
}

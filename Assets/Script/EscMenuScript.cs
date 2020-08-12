using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject escMenu;

    public void LoadStartMenu()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(MoveCamera(new Vector2(0, 0)));
            escMenu.SetActive(false);
        } else
        {
            SceneManager.LoadScene("Main");
            escMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.active)
            {
                escMenu.SetActive(false);
            } else
            {
                escMenu.SetActive(true);
            }
        }
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

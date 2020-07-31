using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleScript : MonoBehaviour
{
    public float gravityScale;
    public float gravityBound;

    public float objGravityScale;
    public float objGravityBound;

    public float firstDragTime;
    public float firstDragRate;

    private Rigidbody2D playerRB;
    private Transform playerTF;

    private GameObject[] Boxs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        Boxs = GameObject.FindGameObjectsWithTag("Box");
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        playerTF = playerGO.GetComponent<Transform>();

        StartCoroutine(Accelation(firstDragTime));
    }

    IEnumerator Accelation(float time)
    {
        float tempScale = gravityScale;
        float tempObjScale = objGravityScale;

        gravityScale *= firstDragRate;
        objGravityScale *= firstDragRate;

        yield return new WaitForSeconds(time);

        gravityScale = tempScale;
        objGravityScale = tempObjScale;
    }

    // Update is called once per frame
    void Update()
    {
        float length = Vector3.Distance(this.transform.position, playerTF.position);

        if (length < gravityBound)
        {
            Vector2 gravityPos = new Vector2(playerTF.position.x - this.transform.position.x, playerTF.position.y - this.transform.position.y).normalized;
            playerRB.AddForce(gravityScale * gravityPos * Mathf.Pow(Mathf.InverseLerp(gravityBound, 0, length), 2));
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Box"))
        {
            length = Vector2.Distance(this.transform.position, obj.transform.position);
            if (length < objGravityBound)
            {
                Vector2 objGravityPos = new Vector2(obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y).normalized;
                obj.GetComponent<Rigidbody2D>().AddForce(objGravityScale * objGravityPos * Mathf.Pow(Mathf.InverseLerp(objGravityBound, 0, length), 2));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public float gravityScale;
    public float gravityBound;

    private Rigidbody2D playerRB;
    private Transform playerTF;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        playerTF = playerGO.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float length = Vector3.Distance(this.transform.position, playerTF.position);

        if (length < gravityBound)
        {
            Vector2 gravityPos = new Vector2(this.transform.position.x - playerTF.position.x, this.transform.position.y - playerTF.position.y).normalized;
            playerRB.AddForce(gravityScale * gravityPos * Mathf.Pow(Mathf.InverseLerp(gravityBound, 0, length), 2));
        }
    }
}

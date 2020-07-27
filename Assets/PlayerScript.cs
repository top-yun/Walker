using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float jumpPower;
    public float speed;

    WaitForSeconds jumpDelay;
    WaitForSeconds jumpCooltime;

    // Start is called before the first frame update
    private Rigidbody2D rd2d;
    private bool isGround;
    private bool facingRight;
    private Animator anime;
    private float leftright = 0;
    private bool jumpLock;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        jumpDelay = new WaitForSeconds(0.3f);
        jumpCooltime = new WaitForSeconds(0.05f);

        isGround = false;
        facingRight = true;
        jumpLock = true;
    }

    IEnumerator Jump()
    {
        if (jumpLock)
        {
            jumpLock = false;
            Debug.Log("jump1");
            anime.SetBool("isJumping", true);
            yield return jumpDelay;

            Debug.Log("jump2");
            rd2d.velocity += Vector2.up * jumpPower;

            yield return jumpCooltime;

            anime.SetBool("isJumping", false);
            jumpLock = true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MapBlock")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MapBlock")
        {
            isGround = false;
        }
    }

    void animeflip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        leftright = 0;

        leftright += Input.GetKey(rightKey) ? 1 : 0;
        leftright -= Input.GetKey(leftKey) ? 1 : 0;

        if (leftright != 0)
        {
            anime.SetFloat("xSpeed", speed);
        } else
        {
            anime.SetFloat("xSpeed", 0);
        }

        if (leftright > 0 && !facingRight)
            animeflip();
        else if (leftright < 0 && facingRight)
            animeflip();

    }

    private void FixedUpdate()
    {
        rd2d.velocity = new Vector2(leftright * speed * Time.fixedDeltaTime, rd2d.velocity.y);

        if (Input.GetKey(jumpKey) && isGround)
        {
            Debug.Log("jump0");
            isGround = false;
            StartCoroutine(Jump());
        }

    }
}

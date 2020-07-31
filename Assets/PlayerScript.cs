using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float jumpPower;
    public float speed;
    public float DangerHeight;
    public Vector2 responePos;

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
        this.transform.position = responePos;
        rd2d = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        jumpDelay = new WaitForSeconds(0.15f);
        jumpCooltime = new WaitForSeconds(0.5f);

        isGround = false;
        facingRight = true;
        jumpLock = true;
    }

    IEnumerator Jump()
    {
        if (jumpLock)
        {
            jumpLock = false;
            anime.SetBool("isJumping", true);
            yield return jumpDelay;

            rd2d.velocity += Vector2.up * jumpPower;

            yield return jumpCooltime;

            anime.SetBool("isJumping", false);
            jumpLock = true;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D cp in collision.contacts)
        {
            if (Vector2.Dot(cp.normal, Vector2.up) > 0.5)
            {
                isGround = true;
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Spike")
        {
            Reset();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
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

        if (this.transform.position.y < DangerHeight || Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*
        this.transform.position = responePos; // falldown check
        rd2d.velocity = Vector2.zero;
        this.GetComponent<PlanetController>().resetAll();
        */
    }

    private void FixedUpdate()
    {
        rd2d.velocity = new Vector2(leftright * speed * Time.fixedDeltaTime, rd2d.velocity.y);

        if (Input.GetKey(jumpKey) && isGround)
        {
            isGround = false;
            StartCoroutine(Jump());
        }


    }

}

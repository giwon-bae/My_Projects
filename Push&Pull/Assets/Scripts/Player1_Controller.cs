using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Controller : MonoBehaviour
{
    [SerializeField] GameObject TargetObj;

    public float speed = 3f;
    public float jumpForce = 5f;
    public int maxJumpCount = 1;

    private float xAxis;
    private bool jDown;
    private int jumpCount = 0;
    private Rigidbody2D playerRigid;
    private Vector2 moveVec;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        //if (Input.GetButtonDown("Push"))
        //{
        //    TargetObj.transform.Translate(new Vector2(0,2f));
        //}
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jDown = Input.GetButton("Jump1");
    }

    void Move()
    {
        moveVec = new Vector2(xAxis * speed, playerRigid.velocity.y);
        playerRigid.velocity = moveVec;

        if (jDown && jumpCount < maxJumpCount)
        {
            playerRigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
        }
    }
}

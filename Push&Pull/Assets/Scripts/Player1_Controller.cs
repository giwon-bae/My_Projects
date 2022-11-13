using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Controller : MonoBehaviour
{
    [SerializeField] GameObject TargetObj;

    public float speed = 3f;
    public float jumpForce = 5f;
    public float pushFower = 3f;
    public float iDelay = 1f;
    public int maxJumpCount = 1;
    public LayerMask layerMask;

    private float xAxis;
    private float iCurrentTime = 0;
    private bool jDown;
    private bool iDown;
    private int jumpCount = 0;
    private Rigidbody2D playerRigid;
    private Collider2D[] hit;
    private Vector2 moveVec;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        Interaction();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jDown = Input.GetButton("Jump1");
        iDown = Input.GetButton("Interaction1");
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

    void Search()
    {
        hit = Physics2D.OverlapCircleAll(transform.position, 3f, layerMask);
    }

    void Interaction()
    {
        iCurrentTime += Time.deltaTime;
        if (!iDown || iCurrentTime < iDelay) return;

        Search();
        if (hit == null) return;

        iCurrentTime = 0;

        for (int i=0; i<hit.Length; i++)
        {
            //Rigidbody2D hitRigid = hit[i].GetComponent<Rigidbody2D>();
            //hitRigid.AddForce(new Vector2(hit[i].transform.position.x - this.transform.position.x, hit[i].transform.position.y - this.transform.position.y).normalized, ForceMode2D.Impulse);
            //hit[].transform.position = Vector2.MoveTowards(hit[i].transform.position, new Vector2(hit[i].transform.position.x - this.transform.position.x, hit[i].transform.position.y - this.transform.position.y).normalized, 3f*Time.deltaTime);
            
            Vector2 pushVec = new Vector2(hit[i].transform.position.x - this.transform.position.x, hit[i].transform.position.y - this.transform.position.y);
            hit[i].transform.Translate(pushVec.normalized * pushFower);
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

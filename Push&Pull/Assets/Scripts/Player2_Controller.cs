using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Controller : MonoBehaviour
{
    [SerializeField] GameObject TargetObj;

    public float speed = 3f;
    public float jumpForce = 5f;
    public float iDelay = 1f;
    public bool isDead = false;
    public int maxJumpCount = 1;
    public LayerMask layerMask;
    public GameManager gameManager;

    private float xAxis;
    private float iCurrentTime = 0;
    private bool jDown;
    private bool iDown;
    private int jumpCount = 0;
    private Player1_Controller p1Ctrl;
    private Rigidbody2D playerRigid;
    private Collider2D[] hit;
    private Vector2 moveVec;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        p1Ctrl = TargetObj.GetComponent<Player1_Controller>();
    }

    void Update()
    {
        GetInput();
        Interaction();

        if (Input.GetKeyDown(KeyCode.P))
        {
            //Vector2 dirVec = new Vector2(transform.position.x - TargetObj.transform.position.x, transform.position.y - TargetObj.transform.position.y);
            Vector2 dirVec = (transform.position - TargetObj.transform.position).normalized;
            Debug.Log(dirVec);
            //playerRigid.AddForce(dirVec * 50f);
            playerRigid.velocity = dirVec * 10f;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal2");
        jDown = Input.GetButton("Jump2");
        iDown = Input.GetButton("Interaction2");
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

        for (int i = 0; i < hit.Length; i++)
        {
            //Debug.Log("1");
            hit[i].transform.position = transform.position;
        }
    }

    public void Die()
    {
        Debug.Log("Player2 is dead.");
        gameObject.SetActive(false);
        isDead = true;
        TargetObj.SetActive(false);
        p1Ctrl.isDead = true;

        //if (!p1Ctrl.isDead)
        //{
        //    p1Ctrl.Die();
        //}

        gameManager.InitializingObject();
    }

    public void VelocityZero()
    {
        playerRigid.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            gameManager.check_p2 = true;
            gameManager.NextStage();
        }
        else if (collision.tag == "Key")
        {
            gameManager.GetKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Dead")
        {
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            gameManager.check_p2 = false;
        }
    }
}

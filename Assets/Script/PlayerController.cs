using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpspeed = 20;
    public float atkjump = 10;
    public bool jumping = false;
    public float gravityScale = 10;
    public bool isGround;
    bool isUnbeattime = false;
    Rigidbody2D rigidbody;
    Vector3 movement;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    public int hp = 100;
    public Animator animator;
    public GameObject leg1;
    public GameObject leg2;
    SpriteRenderer legrenderer1;
    SpriteRenderer legrenderer2;
    GameObject GameDirector;
    AudioSource ohmygod;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        rigidbody.gravityScale = this.gravityScale;
        rigid = gameObject.GetComponent <Rigidbody2D> ();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        legrenderer1 = leg1.gameObject.GetComponent<SpriteRenderer>();
        legrenderer2 = leg2.gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        GameDirector = GameObject.Find("GameDirector");
        ohmygod = gameObject.GetComponent<AudioSource>();
        
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
      

        if (col.transform.tag == "monster" && rigid.velocity.y < -8f)
        {
            AlienMove alien = col.GetComponent<AlienMove>();
            alien.Die();
            Debug.Log("공격했다");
            rigidbody.velocity = Vector3.up * atkjump;

        }
        if (col.transform.tag == "kimchi" && !isUnbeattime)
        {
            Destroy(col.gameObject);
            Debug.Log("오마이갓김치");
            ohmygod.Play();
            GameDirector.GetComponent<Director>().DecreaseHp();
           

            isUnbeattime = true;
            StartCoroutine("UnBeatTime");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
      
        if (col.transform.tag == "monster" && !isUnbeattime)
        {
            Debug.Log("공격 당했다");
            ohmygod.Play();
            
            GameDirector.GetComponent<Director>().DecreaseHp();
            Vector2 attaked = Vector2.zero;
            if (col.gameObject.transform.position.x > transform.position.x)
                attaked = new Vector2(-10f, 10f);
            else
                attaked = new Vector2(10f, 10f);
            rigid.AddForce(attaked, ForceMode2D.Impulse);

            isUnbeattime = true;
            StartCoroutine("UnBeatTime");
        }
      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.isGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = 0;
            h =Input.GetAxisRaw("Horizontal");
        
        animator.SetInteger("flag", (int)h);
        if (transform.position.x <= -5.83 && h == -1)
        {
            h = 0;
         
        }
        else if (transform.position.x >= 5.83 && h == 1) {
            h = 0;
           

        };




        transform.Translate(Vector3.right * speed * h * Time.deltaTime);
       

        if (Input.GetButtonDown("Jump") && isGround)
        {
            Debug.Log("Jump");
            
            rigidbody.velocity = Vector3.up * jumpspeed;
        }

       
        

    }

    void Run(float h, float v)
    {
        movement.Set(h, 0, 0);
       // transform.Translate(Vector3.right * speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);

       // movement = movement.normalized * speed * Time.deltaTime;

        //rigidbody.MovePosition(transform.position + movement);
    }

    void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (countTime % 2 == 0) { 
            spriterenderer.color = new Color32(255, 255, 255, 90);
            legrenderer1.color = new Color32(255, 255, 255, 90);
            legrenderer2.color = new Color32(255, 255, 255, 90);
        }
            else { 
                spriterenderer.color = new Color32(255, 255, 255, 180);
                legrenderer1.color = new Color32(255, 255, 255, 180);
                legrenderer2.color = new Color32(255, 255, 255, 180);
                }
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }

        spriterenderer.color = new Color32(255, 255, 255, 255);
        legrenderer1.color = new Color32(255, 255, 255, 255);
        legrenderer2.color = new Color32(255, 255, 255, 255);
        isUnbeattime = false;
        yield return null;
    }
    public void Die()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.flipY = true;
        leg1.gameObject.active = false;
        leg2.gameObject.active = false;
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;

        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 4;
        Vector2 diePlayer = new Vector2(0, 10f);
        rigidbody.AddForce(diePlayer, ForceMode2D.Impulse);

        Destroy(gameObject, 5f);
    }
}

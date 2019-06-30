using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    
    public float movePower = 1f;
    bool leftwall = false;
    bool rightwall = false;
    int[] leftend = { 0, 2 };
    int[] rightend = { 0, 1 };
    
    public Animator animator;
    Vector3 movement;
    int movementFlag = 0; // 0:idle, 1:left, 2:right

    AudioSource ssagaji;
    
    void Start()
    {

        ssagaji = gameObject.GetComponent<AudioSource>();
        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);
        if (leftwall)
        {
            movementFlag = leftend[Random.Range(0, leftend.Length)];
        }
        else if (rightwall)
        {
           movementFlag = rightend[Random.Range(0, rightend.Length)];
         }
        
        animator.SetInteger("alienflag", movementFlag);

        yield return new WaitForSeconds(2f);

        StartCoroutine("ChangeMovement");
    }

    void Move()
    {
        Vector3 moveAlien = Vector3.zero;

        if (movementFlag == 1)
        {
            moveAlien = Vector3.left;
       
        }
        else if(movementFlag == 2)
        {
            moveAlien = Vector3.right;
           
        }
        transform.position += moveAlien * movePower * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "leftwall")
        {
            this.leftwall = true;
        }
        else if (collision.transform.tag == "rightwall")
        {
            this.rightwall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "leftwall")
        {
            this.leftwall = false;
        }
        else if (collision.transform.tag == "rightwall")
        {
            this.rightwall = false;
        }
    }
    public void Die()
    {
        ssagaji.Play();
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.flipY = true;
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;
        CapsuleCollider2D coll2 = gameObject.GetComponent<CapsuleCollider2D>();
        coll2.enabled = false;

        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 4;
        Vector2 dieAlien = new Vector2(0, 10f);
        rigidbody.AddForce(dieAlien, ForceMode2D.Impulse);
        
        Destroy(gameObject,5f);
    }
}

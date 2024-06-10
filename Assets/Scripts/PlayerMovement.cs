using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

[Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private BoxCollider2D collider;
    //private PlayerAttack attack;
    public int health = 3;

    // Start is called before the first frame update
    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        //attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        //jumping script
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 12);
            //isGrounded = false;
            animator.SetTrigger("Jump");
        }
        
        if (!onWall()){
            rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);

        }


    //flips the player direction
        if (horizontal == -1){
            transform.localScale = new Vector3(-6, 6, 6);
            
        }else if (horizontal == 1){
            transform.localScale = new Vector3(6,6,6);

        }

        //walking animatrion
        animator.SetBool("Walk", horizontal != 0);
        animator.SetBool("Grounded", isGrounded());

        //print(onWall());
    }

    private void OnCollisionEnter2D(Collision2D other){
        
    }

    public bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    public bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return isGrounded();
    }
}

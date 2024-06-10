using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject attackRange;
    public float speed;
    [Header("Layers")]
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public LayerMask entityLayer;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    public PlayerMovement playerMov;
    public float detectionDistance;
    public int health = 2;
    private Animator animator;
    public HealthBar healthBar;

    [Header("Attack Modifiers")]
    private float cooldownTimer = Mathf.Infinity;
    public float attackCooldown;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);//updates the healthbar

        if (player.transform.position.x <= gameObject.transform.position.x){//left

            transform.localScale = new Vector3(-6,6,6);

            transform.position += -transform.right * Time.deltaTime * speed;
            animator.SetBool("Walk", true);


        }else if (player.transform.position.x > gameObject.transform.position.x){//right

            transform.localScale = new Vector3(6,6,6);

            transform.position += transform.right * Time.deltaTime * speed;
            animator.SetBool("Walk", true);

            
        }else {
            animator.SetBool("Walk", false);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectionDistance, wallLayer);
        if (hit.collider != null && isGrounded())
        {
            // Wall detected, jump
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 12);
            animator.SetTrigger("Jump");

        }
        


        
        if (health <= 0){
            Destroy(gameObject);
        }

        
        animator.SetBool("Grounded", isGrounded());

        cooldownTimer += Time.deltaTime;

        if (detectPlayer() && cooldownTimer > attackCooldown){ //ATTACK CODE
            attackRange.SetActive(true);
            cooldownTimer = 0;
            speed = 0f;
            Invoke("Hide", 0.3f);
        }else{
            attackRange.SetActive(false);
        }
        
    }
    public bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other){ //code for getting damaged
        if (other.tag == "PlayerRange"){
            health -= 1;
            
        }
    }

    public bool detectPlayer(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.5f, entityLayer);
        return raycastHit.collider != null;
    }
    void Hide(){
        attackRange.SetActive(false);
        speed = 2f;
    }
}

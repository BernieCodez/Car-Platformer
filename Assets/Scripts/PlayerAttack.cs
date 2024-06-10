using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    private PlayerMovement movement;
    private Animator animator; 
    public float cooldownTimer = Mathf.Infinity;
    public GameObject attackRange;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        attackRange.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && movement.canAttack()){
            Attack();

        }
            cooldownTimer += Time.deltaTime;

    }

    private void Attack(){
        attackRange.SetActive(true);
        animator.SetTrigger("Attack");
        cooldownTimer = 0;
        movement.speed = 0f;
        //attackRange.SetActive(false);
        Invoke("Hide", 0.3f);

    }

    void Hide(){
        attackRange.SetActive(false);
        movement.speed = 5f;
    }
}

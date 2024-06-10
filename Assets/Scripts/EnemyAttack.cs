using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            player.health -= 1;
            animator.SetTrigger("Attack");
        }
    }
}

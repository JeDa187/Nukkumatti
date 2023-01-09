using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float stratTimeBtwAttack;

    public LayerMask whatIsEnemies;

    public Transform attackPos;
    public float attackRange;

    public int damage;

    public Animator animator;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.G)) {
                animator.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position ,attackRange, whatIsEnemies);
                
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    
                }
                    }
            timeBtwAttack = stratTimeBtwAttack;
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

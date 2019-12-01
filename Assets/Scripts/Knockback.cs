using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                //enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                if (collision.CompareTag("enemy"))
                {
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (collision.CompareTag("Player") && collision.isTrigger)
                {
                    collision.GetComponent<Player>().TakeDamage(damage);
                }
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Knockback : NetworkBehaviour
{
    public float thrust;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
        if (hit != null)
        {
            if (collision.CompareTag("Dragon"))
            {
                collision.GetComponent<Dragon>().TakeDamage(damage);
            }
            else if (collision.CompareTag("log"))
            {
                collision.GetComponent<log>().TakeDamage(damage);
            }
            else if (collision.CompareTag("Player") && collision.isTrigger)
            {
                collision.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}

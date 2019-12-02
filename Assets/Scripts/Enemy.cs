using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : NetworkBehaviour
{
    public EnemyState currentState;
    public Rigidbody2D myRigidBody;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        //health = maxHealth.initialValue;
    }

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //health = maxHealth.initialValue;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}

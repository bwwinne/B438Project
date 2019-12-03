using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : NetworkBehaviour
{
    [SerializeField]
    public Text HPDisplay;
    public EnemyState currentState;
    public Rigidbody2D myRigidBody;
    [SyncVar]
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Server]
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //health = maxHealth.initialValue;
    }

    [Server]
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    [Server]

    private void Update()
    {
        HPDisplay.text = "HP:" + health;
    }

    private void RespawnEnemy()
    {
    }
}

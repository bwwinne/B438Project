using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    public Text HPDisplay;

    public Rigidbody2D myRigidBody;

    [SyncVar]
    public float health;

    [Server]
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    [Server]
    private void Update()
    {
        HPDisplay.text = "HP:" + health;
    }
}

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
    public Vector3 spawnPosition;
    public float spawnHealth;

    [SyncVar]
    public float health;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        spawnPosition = this.gameObject.transform.position;
        spawnHealth = health;
    }

    [Server]
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            Invoke("RpcRespawnPlayer", 3f);
        }
    }

    //[Server]
    private void Update()
    {
        HPDisplay.text = "HP:" + health;
    }

    [ClientRpc]
    private void RpcRespawnPlayer()
    {
        this.gameObject.SetActive(true);
        transform.position = spawnPosition;
        health = spawnHealth;
    }
}

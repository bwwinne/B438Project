using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dragon : Enemy
{
    public Transform homePosition;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Animator anim;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;
    private float spawnHealth;

    /*void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
    } */

    [Server]
    public override void OnStartServer()
    {
        Invoke("GetTargets", 1f);
        Invoke("SaveInitialValues", 1f);
    }

    private void SaveInitialValues()
    {
        spawnPosition = this.gameObject.transform.position;
        spawnRotation = this.gameObject.transform.rotation.eulerAngles;
        spawnHealth = health;
    }

    //[Server]
    private void Update()
    {
        HPDisplay.text = "HP:" + health;
        CheckDistance();
    }

    [Server]
    public void GetTargets()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
    }

    [Server]
    void CheckDistance()
    {
        //Debug.Log(Vector3.Distance(target.position, transform.position));
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            //myRigidBody.MovePosition(temp);

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            RotateTowardsTarget();
            ChangeState(EnemyState.walk);
            anim.SetBool("attacking", true);
            //Debug.Log("in range");
            //anim.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, transform.position) < attackRadius)
        {
            RotateTowardsTarget();
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            //ChangeState(EnemyState.idle);
            anim.SetBool("attacking", false);
        }
    }

    [Server]
    private void RotateTowardsTarget()
    {
        float speed = 5.0f * Time.deltaTime;
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed);
    }

    [Server]
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    [Server]
    private void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        //anim.SetFloat("moveX", direction.x);
        //anim.SetFloat("moveY", direction.y);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            Invoke("RespawnEnemy", 5f);
        }
    }

    private void RespawnEnemy()
    {
        this.gameObject.SetActive(true);
        GetTargets();
        this.gameObject.transform.position = spawnPosition;
        this.gameObject.transform.rotation = Quaternion.Euler(spawnRotation);
        health = spawnHealth;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public Transform homePosition;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Animator anim;

    /*void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
    } */

    public override void OnStartServer()
    {
        Invoke("GetTargets", 1f);
    }

    void GetTargets()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        Debug.Log(Vector3.Distance(target.position, transform.position));
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                //ChangeAnim(temp - transform.position);
                //myRigidBody.MovePosition(temp);

                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                RotateTowardsTarget();
                ChangeState(EnemyState.walk);
                anim.SetBool("attacking", true);
                Debug.Log("in range");
                //anim.SetBool("wakeUp", true);
            }

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

    private void RotateTowardsTarget()
    {
        float speed = 5.0f * Time.deltaTime;
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed);
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }


    private void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        //anim.SetFloat("moveX", direction.x);
        //anim.SetFloat("moveY", direction.y);
    }

    private IEnumerator FlameBreath()
    {
        yield return new WaitForSeconds(1f);
    }
}

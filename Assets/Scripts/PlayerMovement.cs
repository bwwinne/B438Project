using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Vector3 playerRot;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        playerRot = Vector3.zero;
        playerRot.x = Input.GetAxisRaw("RightStickX");
        playerRot.y = Input.GetAxisRaw("RightStickY");
        //Debug.Log(change);



        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            //anim.SetFloat("moveX", change.x);
            //anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        // only rotate if right stick input active
        if (playerRot != Vector3.zero)
        {
            RotateCharacter();
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void RotateCharacter()
    {
        //transform.eulerAngles = new Vector3(0,0, Mathf.Atan();
        float horizontal = Input.GetAxisRaw("RightStickX");
        float vertical = Input.GetAxisRaw("RightStickY");
        float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.Rotate(0, 0, 90);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Vector3 playerRot;

    private Animator anim;
    private PlayerState currentState;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim.SetFloat("stickPosX", 0);
        anim.SetFloat("stickPosY", -1);
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraMovement>().SetTarget(this.gameObject.transform);
    }

    void Update()
    {
        if (this.isLocalPlayer)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            playerRot = Vector3.zero;
            playerRot.x = Input.GetAxisRaw("RightStickX");
            playerRot.y = Input.GetAxisRaw("RightStickY");
            // only attack if not already attacking
            if (Input.GetButtonDown("attack") && !anim.GetBool("attacking"))
            {
                StartCoroutine(AttackCoroutine());
            }
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        // wait 1 frame
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.5f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        // only rotate if right stick input active
        if (playerRot != Vector3.zero)
        {
            anim.SetFloat("stickPosX", playerRot.x);
            anim.SetFloat("stickPosY", playerRot.y);
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}

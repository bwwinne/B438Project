using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraMovement : NetworkBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;


    void Start()
    {

    }

    public override void OnStartServer()
    {
        //target = GameObject.FindWithTag("Player").transform;
        Invoke("SetTarget", 1f);
    }

    public void SetTarget()
    {
        //target = player;
        target = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

        }
    }
}

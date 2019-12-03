using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraMovement : NetworkBehaviour
{
    public Transform cameraTarget;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;

    public void SetTarget(Transform target)
    {
        cameraTarget = target;
    }

    void LateUpdate()
    {
        if (transform.position != cameraTarget.position)
        {
            Vector3 targetPos = new Vector3(cameraTarget.position.x, cameraTarget.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform target;
    public float smoothing;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothing);
        }
    }
}

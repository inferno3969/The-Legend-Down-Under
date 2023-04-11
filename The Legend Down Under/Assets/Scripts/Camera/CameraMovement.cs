using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;


    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                                     target.position.y,
                                     transform.position.z);
            transform.position = Vector3.Lerp(transform.position,
                                             targetPosition, smoothing);
            //transform.position = Vector3.Lerp(transform.position,
            //                                 targetPosition, smoothing);
        }
    }
}

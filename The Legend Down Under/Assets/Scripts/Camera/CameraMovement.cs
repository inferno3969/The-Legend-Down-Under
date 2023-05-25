using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Animator")]
    public Animator animator;

    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    // Use this for initialization
    private void Start()
    {
        if (maxPosition != null && camMax != null)
        {
            maxPosition = camMax.runtimeValue;
        }
        if (minPosition != null && camMin != null)
        {
            minPosition = camMin.runtimeValue;
        }
        animator = GetComponent<Animator>();
        // if (target != null)
        // {
        //     if (followPlayer)
        //     {
        //         transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //     }
        // }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x,
                                                     target.position.y,
                                                     transform.position.z);
                targetPosition.x = Mathf.Clamp(targetPosition.x,
                                               minPosition.x,
                                               maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y,
                                               minPosition.y,
                                               maxPosition.y);

                transform.position = Vector3.Lerp(transform.position,
                                                 targetPosition, smoothing);
            }
        }
    }

    public void BeginKick()
    {
        animator.SetBool("KickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        animator.SetBool("KickActive", false);
    }
}
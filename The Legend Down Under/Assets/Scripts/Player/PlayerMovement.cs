using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D playerRigidBody;
    private Vector3 change;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (Mathf.Abs(change.x) > Mathf.Abs(change.y))
        {
            change.y = 0;
        }
        else
        {
            change.x = 0;
        }

        UpdateAnimationAndMove();
    }

    void MoveCharacter()
    {
        playerRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change.normalized);
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            transform.Translate(new Vector3(change.x, change.y));
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
}

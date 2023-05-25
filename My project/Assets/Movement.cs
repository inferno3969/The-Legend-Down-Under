using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        // Get horizontal and vertical input values
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to ensure consistent speed in all directions
        movement = movement.normalized;

        // Calculate the movement amount
        float movementAmount = moveSpeed * Time.deltaTime;

        // Move the object
        transform.Translate(movement * movementAmount);
    }
}

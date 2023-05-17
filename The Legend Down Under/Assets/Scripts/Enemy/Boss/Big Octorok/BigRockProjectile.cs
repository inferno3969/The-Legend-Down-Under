using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRockProjectile : Projectile
{
    public void Launch(Vector2 initialVel, int random)
    {
        // if (random == 1)
        // {
        //     moveSpeed = 6;
        // }
        // else if (random == 2)
        // {
        //     moveSpeed = 4;
        // }
        moveSpeed = Random.Range(5, 7);
        myRigidbody.velocity = initialVel * moveSpeed;
    }
}

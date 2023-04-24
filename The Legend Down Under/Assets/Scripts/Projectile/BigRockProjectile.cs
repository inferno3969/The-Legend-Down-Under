using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRockProjectile : Projectile
{
    public override void Launch(Vector2 initialVel)
    {
        // randomize velocity of moveSpeed to make it more fair
        // to prevent Player from just holding down the Shield button
        moveSpeed = Random.Range(5, 8);
        myRigidbody.velocity = initialVel * moveSpeed;
    }
}

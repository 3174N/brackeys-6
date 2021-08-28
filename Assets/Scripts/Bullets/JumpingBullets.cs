using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBullets : Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 oldVelocity = _rb.velocity;

        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, other.contacts[0].normal);

        _rb.velocity = reflectedVelocity;

        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
        transform.rotation = rotation * transform.rotation;
    }
}

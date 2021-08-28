using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directed : MonoBehaviour, ShootingMethod
{
    public Transform DirectedTransform;
    public GameObject BulletPrefab;

    public void Shoot()
    {
        Vector2 direction = transform.position - DirectedTransform.position;
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90));
    }
}

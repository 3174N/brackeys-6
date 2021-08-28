using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour, ShootingMethod
{

    public GameObject BulletPrefab;
    public void Shoot()
    {
        Instantiate(BulletPrefab, transform.position, transform.rotation);
    }
}

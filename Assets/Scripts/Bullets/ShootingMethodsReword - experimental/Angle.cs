using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour, ShootingMethod
{

    // each shooting method can have its own bullet prefab. Is it bad?
    public GameObject BulletPrefab;
    public int BulletAmount;
    public float StartAngle, EndAngle;

    public void Shoot()
    {
        float angleStep = (EndAngle - StartAngle) / (BulletAmount);
        float angle = StartAngle;
        for (int i = 1; i <= BulletAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 bulMove = new Vector2(bulDirX, bulDirY);
            Vector2 bulDir = (bulMove - (Vector2)transform.position).normalized;

            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0f, 0f, Mathf.Atan2(bulDir.y, bulDir.x) * Mathf.Rad2Deg + 90));

            angle += angleStep;
        }
    }
}

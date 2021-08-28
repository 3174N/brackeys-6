using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnExp: MonoBehaviour
{

    public ShootingMethod[] Methods;
    int _cur_method;
    public float SpawnDelay;
    private float _delay;

    public float probSwap = 15f;

    /*
    public GameObject BulletPrefab;
    [Header("Directed Bullets")] 
    public Transform DirectedTransform;

    [Header("Angle")]
    public int BulletAmount;
    public float StartAngle, EndAngle;
    */

    // Start is called before the first frame update
    void Start()
    {
        _delay = SpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay <= 0)
        {
            Shoot();
            _delay = SpawnDelay;
        }
    }

    /// <summary>
    /// Spawns a bullet.
    /// </summary>
    private void Shoot()
    {

        // try to randomly (not really) swap a shooting method from your arsenal
        if (Random.Range(0, 100) <= probSwap)
        {
            _cur_method += 1;
            _cur_method %= Methods.Length; // make it loop through and not exit the range (Arr = [..32..]. i= 31+1->32->0)
        }

        if (Methods.Length > 0)
        {
            Methods[_cur_method].Shoot();
        }
        


        /*
        // Spawn bullet
        switch (Method)
        {
            case ShootingMethod.Line:
                Instantiate(BulletPrefab, transform.position, transform.rotation);
                break;
            case ShootingMethod.Directed:
                Vector2 direction = transform.position - DirectedTransform.position;
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90));
                break;
            case ShootingMethod.Angle:
                float angleStep = (EndAngle - StartAngle) / (BulletAmount);
                float angle = StartAngle;
                for (int i = 0; i < BulletAmount + 1; i++)
                {
                    float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector2 bulMove = new Vector2(bulDirX, bulDirY);
                    Vector2 bulDir = (bulMove - (Vector2)transform.position).normalized;

                    Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0f, 0f, Mathf.Atan2(bulDir.y, bulDir.x) * Mathf.Rad2Deg + 90));

                    angle += angleStep;
                }
                break;
            default:
                break;
        }
        */
    }
}

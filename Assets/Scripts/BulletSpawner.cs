using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public enum ShootingMethod
    {
        Line,
        Directed,
    };

    public ShootingMethod Method;
    public float SpawnDelay;
    private float _delay;
    public GameObject BulletPrefab;
    public Transform DirectedTransform;

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
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public enum ShootingMethod
    {
        Line,
    };

    public ShootingMethod Method;
    public float SpawnDelay;
    private float _delay;
    public GameObject BulletPrefab;

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
        Instantiate(BulletPrefab, transform.position, transform.rotation);
    }
}

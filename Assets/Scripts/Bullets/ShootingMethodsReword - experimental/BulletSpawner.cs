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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Storage
{
    public float CookTime;
    private float _currentTime;
    private bool _isCooking = false;

    private void Start()
    {
        _currentTime = CookTime;
    }

    public override void OnCollectAll()
    {
        base.OnCollectAll();

        _isCooking = true;
    }

    private void Update()
    {
        if (_isCooking)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                // TODO: something
            }

            // TODO: progress bar
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Storage
{
    public float CookTime;
    private float _currentTime;

    private void Start()
    {
        _currentTime = CookTime;

        CurrentItems = 0;

        Bar.Minimum = 0;
        Bar.Maximum = Items;
        Bar.Current = CurrentItems;
    }

    public override void OnCollectAll()
    {
        base.OnCollectAll();

        Bar.Maximum = CookTime;
        Bar.Minimum = 0f;
        Bar.Current = _currentTime;
    }

    private void Update()
    {
        if (_isFull)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                FindObjectOfType<GameManager>().Level3 = true;
                FindObjectOfType<GameManager>().Load();
            }

            Bar.Current = _currentTime;
        }
    }
}

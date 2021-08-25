using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int Items;
    [HideInInspector] public int CurrentItems;
    [HideInInspector] public bool _isFull = false;
    public ProgressBar Bar;

    private void Start()
    {
        CurrentItems = 0;

        Bar.Minimum = 0;
        Bar.Maximum = Items;
        Bar.Current = CurrentItems;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            PlayerController player = other.transform.parent.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ReleaseItem();
                Destroy(other.gameObject);
                OnCollect();
            }
        }
    }

    /// <summary>
    /// Called when an item is collected.
    /// </summary>
    public virtual void OnCollect()
    {
        CurrentItems++;

        Bar.Current = CurrentItems;

        if (CurrentItems >= Items)
            OnCollectAll();
    }

    /// <summary>
    /// Called when all items are collected.
    /// </summary>
    public virtual void OnCollectAll()
    {
        _isFull = true;
    }
}

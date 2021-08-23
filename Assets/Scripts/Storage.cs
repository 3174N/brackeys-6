using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int Items;
    private int _currentItems;

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
    public virtual void OnCollect()
    {
        _currentItems++;

        if (_currentItems >= Items)
            OnCollectAll();
    }

    public virtual void OnCollectAll() { }
}

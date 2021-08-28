using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    private Rigidbody2D _rb;
    public int Damage = 20;

    private void Awake()
    {
        // Get components
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Move bullet
        _rb.velocity = transform.up * Speed;
        LifeTime = 13f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if bullet hit player
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            // Damage player
            player.DamagePlayer(Damage);
        }

        // Destroy bullet
        Destroy(gameObject);
    }
}

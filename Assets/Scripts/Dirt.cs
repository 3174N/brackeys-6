using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int DirtLevel = 5;
    private int _currentDirt;
    private PlayerController _player = null;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentDirt = DirtLevel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            // Player entered collider
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            // Player exited collider
            _player = null;

            ReduceDirt(player.CleaningAmount);
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            // Player is in collider
            // if (Input.GetKeyDown(_player.CleaningKey))
            //     ReduceDirt(_player.CleaningAmount); // Clean dirt
        }
    }

    /// <summary>
    /// Reduces the dirt level.
    /// </summary>
    /// <param name="amount">How much to reduce.</param>
    private void ReduceDirt(int amount)
    {
        _currentDirt -= amount;

        // Change sprite opacity
        Color color = _renderer.material.color;
        color.a = (float)_currentDirt / (float)DirtLevel;
        _renderer.material.color = color;

        if (_currentDirt <= 0)
            RemoveDirt();
    }

    /// <summary>
    /// Removes the dirt.
    /// </summary>
    private void RemoveDirt()
    {
        // TODO: particals?
        Destroy(gameObject);
    }
}

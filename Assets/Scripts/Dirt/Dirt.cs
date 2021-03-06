using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int DirtLevel = 5;
    private int _currentDirt;
    private SpriteRenderer _renderer;
    public GameObject Sparkle;
    public bool IsTut = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentDirt = DirtLevel;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            // Player exited collider
            ReduceDirt(player.CleaningAmount);
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
        GameObject sparkle = Instantiate(Sparkle, transform.position, transform.rotation);
        Destroy(sparkle, 3f);
        if (!IsTut)
            FindObjectOfType<DirtCounter>().RemoveDirt();
        Destroy(gameObject);
    }
}

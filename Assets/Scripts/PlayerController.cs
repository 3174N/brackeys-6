using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth = 100f;
    private float _currentHealth;
    public ProgressBar HealthBar;

    [Header("Movement")]
    public float Speed = 3f;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    [Header("Cleaning")]
    public int CleaningAmount = 1;

    [Header("Carrying")]
    public KeyCode CarryKey = KeyCode.E;
    public Transform CarryLocation;
    private Transform _currentItem = null;
    private bool _isInRange = false, _isCarrying = false;

    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;

        HealthBar.Maximum = MaxHealth;
        HealthBar.Minimum = 0f;
        HealthBar.Current = _currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement.Set(horizontal, vertical);
        _movement.Normalize();

        if (_movement != Vector2.zero)
        {
            // Player animations
            _animator.SetFloat("LookX", _movement.x);
            _animator.SetFloat("LookY", _movement.y);
        }
        _animator.SetFloat("Speed", _movement.magnitude);

        // Carrying
        if (_movement != Vector2.zero)
            CarryLocation.localPosition = _movement;

        if (Input.GetKeyDown(CarryKey))
        {
            if (_isCarrying)
            {
                ReleaseItem();
            }
            else
            {
                // Pick up item
                _currentItem.position = CarryLocation.position;
                _currentItem.parent = CarryLocation;

                _isCarrying = true;
            }
        }
    }

    public void ReleaseItem()
    {
        // Release item
        _currentItem.parent = null;
        _currentItem = null;

        _isCarrying = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // pickup if it has tag "Item" and we are not carrying anything
        if (other.CompareTag("Item") && _currentItem == null && !_isCarrying)
        {
            _currentItem = other.transform;
            _isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == _currentItem && !_isCarrying)
        {
            _currentItem = null;
            _isInRange = false;
        }
    }

    private void FixedUpdate()
    {
        // Set player velocity based on _movement
        _rb.velocity = new Vector2(_movement.x * Speed, _movement.y * Speed);
    }

    /// <summary>
    /// Reduces player HP.
    /// </summary>
    /// <param name="amount">How much HP to reduce.</param>
    public void DamagePlayer(float amount)
    {
        _currentHealth -= amount;

        HealthBar.Current = _currentHealth;
    }
}

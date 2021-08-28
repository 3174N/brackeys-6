using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth = 100f;
    public ProgressBar HealthBar;
    // iframes needs a revision later.
    public int _iframes = 12, _curIframes;

    [Header("Movement")]
    public float Speed = 5.7f;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    [Header("Cleaning")]
    public int CleaningAmount = 1;

    [Header("Carrying")]
    public KeyCode CarryKey = KeyCode.E;
    public Transform CarryLocation;
    private Transform _currentItem = null;
    private bool _isCarrying = false;

    [Header("Dodging")]
    public KeyCode DodgeKey = KeyCode.Space;
    bool _isDodging = false;
    public float _dodgeTime = 0.5f, _cooldownTime = 1f, DodgeSpeedMult = 0.8f;
    float _dodgeTimer, _cooldownTimer;


    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.Maximum = MaxHealth;
        HealthBar.Minimum = 20f;
        HealthBar.Current = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleCarrying();
    }

    public void HandleMovement()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement.Set(horizontal, vertical);
        _movement.Normalize();

        // disgusting solution for now
        HandleDodgeTimer();

        if (_movement != Vector2.zero)
        {

            // try dodging
            // issue with it. On the last dodge frame _isDodging is false so it won't render the animation.
            HandleDodge();

            // if you didn't dodge
            if (!_isDodging) { 
                // else play walking animations
                _animator.SetFloat("LookY", _movement.y);
                _animator.SetFloat("LookX", _movement.x);
                _animator.SetFloat("Speed", _movement.magnitude);
            }
        }
    }

    public void HandleCarrying()
    {
        // make carry location follow movement
        if (_movement != Vector2.zero)
            CarryLocation.localPosition = _movement;

        // drop or carry item
        if (Input.GetKeyDown(CarryKey))
        {
            if (_isCarrying) {ReleaseItem();}
            else if (_currentItem)
            {
                // place currently picked up item in your hand
                _currentItem.position = CarryLocation.position;
                _currentItem.parent = CarryLocation;
                _isCarrying = true;
            }
        }
    }

    void HandleDodge()
    {
        // if you're trying to dodge and can, do it
        if (!_isDodging && _cooldownTimer == 0 && Input.GetKeyDown(DodgeKey))
        {
            _isDodging = true;
            _dodgeTimer = _dodgeTime;
            _curIframes = Mathf.Max(_curIframes, _iframes);
            _cooldownTimer = _cooldownTime;
            _animator.SetTrigger("Dodging");
        }

        if (_isDodging)
        {
            // set movement to lock in dodge direction
            _movement = _rb.transform.forward;
            Debug.Log("Setting speed to" + _movement);
        }
    }

    void HandleDodgeTimer()
    {
        // take care of cooldown
        _cooldownTimer = Mathf.Max(0f, _cooldownTimer - Time.deltaTime);

        if (_isDodging)
        {
            _dodgeTimer = Mathf.Max(0f, _dodgeTimer - Time.deltaTime);

            // uh oh ran out of dodge time
            if (_dodgeTimer == 0)
            {
                _isDodging = false;
                _animator.SetBool("Dodging", false);
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
        if (other.CompareTag("Item") && _currentItem == null)
        {
            _currentItem = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == _currentItem && !_isCarrying)
        {
            _currentItem = null;
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
    public void DamagePlayer(float damage)
    {
        if (_curIframes == 0)
        {
            HealthBar.Current -= damage;
        }
    }
}

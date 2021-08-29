using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth = 100f;
    private float _currentHealth;
    public ProgressBar HealthBar;
    // iframes needs a revision later.
    public float _iframes = 1f;
    float _curIframes;

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
    public float _dodgeAmount = 0.583f, _cooldownTime = 0.8f, DodgeSpeedMult = 1.2f;
    float _dodgeTimer, _cooldownTimer;

    public GameObject DeathMenu;

    Vector2 _dodge_movement; // workaround


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
        HealthBar.Current = MaxHealth;

        // hacky solution
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("IgnoreBullet"), LayerMask.NameToLayer("Bullet"));
    }

    // Update is called once per frame
    void Update()
    {
        HandleIframes();
        HandleMovement();
        HandleCarrying();
    }

    public void HandleIframes()
    {
        if (_curIframes == 0) { gameObject.layer = LayerMask.NameToLayer("Default"); }
        _curIframes = Mathf.Max(0, _curIframes - Time.deltaTime);
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

            // don't set overall position during dodge
            if (!_isDodging)
            {
                _animator.SetFloat("LookY", _movement.y);
                _animator.SetFloat("LookX", _movement.x);
            }
        }
        _animator.SetFloat("Speed", _movement.magnitude);
    }

    public void HandleCarrying()
    {
        // make carry location follow movement
        if (_movement != Vector2.zero)
            CarryLocation.localPosition = _movement;

        // drop or carry item
        if (Input.GetKeyDown(CarryKey))
        {
            if (_isCarrying) { ReleaseItem(); }
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

            // save last movement to lock it in dodge
            _dodge_movement = new Vector2(_movement.x, _movement.y);
            _isDodging = true;
            _dodgeTimer = _dodgeAmount;

            // set iframes (don't interfere with other iframes), cd and animation
            _curIframes = Mathf.Max(_curIframes, _iframes);
            gameObject.layer = LayerMask.NameToLayer("IgnoreBullet"); // ignore bullets on dodge
            _cooldownTimer = _cooldownTime;
            _animator.SetBool("IsDodging", true);
        }

        if (_isDodging)
        {
            // set movement to lock in dodge direction
            _movement = _dodge_movement * DodgeSpeedMult;
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
                _animator.SetBool("IsDodging", false);
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
            _curIframes = 1f;
            _currentHealth -= damage;

            if (_currentHealth <= 0) Die();
        }
    }
    private void Die()
    {
        Time.timeScale = 0;
        DeathMenu.SetActive(true);
    }
}

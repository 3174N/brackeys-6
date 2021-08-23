using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 3f;
    private Vector2 _movement;
    private Rigidbody2D _rb;


    [Header("Cleaning")]
    public KeyCode CleaningKey = KeyCode.E;
    public int CleaningAmount = 1;

    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement.Set(horizontal, vertical);
        _movement.Normalize();

        // Player animations
        _animator.SetFloat("LookX", _movement.x);
        _animator.SetFloat("LookY", _movement.y);
        _animator.SetFloat("Speed", _movement.magnitude);
    }

    private void FixedUpdate()
    {
        // Set player velocity based on _movement
        _rb.velocity = new Vector2(_movement.x * Speed, _movement.y * Speed);
    }
}

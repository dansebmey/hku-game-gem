using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private ParallaxBackground _parallaxBackground;
    
    public float defaultJumpForce;
    private float _jumpForce;

    private Animator animator;

    protected bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    protected IXArea ixArea;
    private ToyBlock _carriedBlock;
    public ToyBlock CarriedBlock
    {
        get => _carriedBlock;
        set
        {
            _carriedBlock = value;
            CurrentState = value == null ? State.DEFAULT : State.CARRYING_AN_ITEM;
        }
    }

    internal Vector2 dir;

    public enum State { DEFAULT, CARRYING_AN_ITEM }
    private State _currentState = State.DEFAULT;
    public State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case State.DEFAULT:
                    moveSpeed = defaultMoveSpeed;
                    _jumpForce = defaultJumpForce;
                    break;
                case State.CARRYING_AN_ITEM:
                    moveSpeed = defaultMoveSpeed * 1;
                    _jumpForce = defaultJumpForce * 1;
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponentInChildren<Animator>();

        ixArea = GetComponentInChildren<IXArea>();
    }

    protected override void Start()
    {
        base.Start();

        _jumpForce = defaultJumpForce;
    }

    protected override void Update()
    {
        base.Update();
        
        // HandleGravity();
        HandleInput();

        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    private void HandleInput()
    {
        HandleHorizontalMovement();
        HandleJump();
        HandleInteraction();
        
        _parallaxBackground.Move(rb.velocity.x);
    }

    private void HandleGravity()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    private void HandleHorizontalMovement()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xSpeed * moveSpeed, rb.velocity.y);

        animator.SetFloat("HorizontalSpeed", rb.velocity.x);

        if (xSpeed > 0)
        {
            dir = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xSpeed < 0)
        {
            dir = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CarriedBlock == null)
            {
                ixArea.propInFocus?.OnPickup(this);
            }
            else
            {
                CarriedBlock.OnDrop(this);
            }
        }
    }
}
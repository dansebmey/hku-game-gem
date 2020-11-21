using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public float jumpForce;
    
    protected bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    protected IXArea ixArea;
    private ToyBlock _carriedItem;
    public ToyBlock CarriedItem
    {
        get => _carriedItem;
        set => _carriedItem = value;
    }

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
                    moveSpeed = GameConstants.defaultMoveSpeed;
                    jumpForce = GameConstants.defaultJumpForce;
                    break;
                case State.CARRYING_AN_ITEM:
                    moveSpeed = GameConstants.moveSpeedWhenCarrying;
                    jumpForce = GameConstants.jumpForceWhenCarrying;
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        ixArea = GetComponentInChildren<IXArea>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
        HandleInput();
    }

    private void HandleInput()
    {
        HandleGravity();
        HandleHorizontalMovement();
        HandleJump();
        HandleInteraction();
    }

    private void HandleGravity()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    private void HandleHorizontalMovement()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xSpeed * moveSpeed, rb.velocity.y);

        if (xSpeed > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xSpeed < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            if (CarriedItem == null)
            {
                ixArea.propInFocus?.OnPickup(this);
            }
            else
            {
                CarriedItem.OnDrop(this);
            }
        }
    }
}
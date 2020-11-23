using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    #region Attributes
    
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
            if (value == null)
            {
                CurrentState = State.DEFAULT;
            }
            else
            {
                CurrentState = State.CARRYING_AN_ITEM;
                _carriedBlock.ShowTooltip = false;
            }
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
    
    #endregion

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

    protected virtual void Update()
    {
        if (GameManager.gameState == GameManager.GameState.PLAYING)
        {
            HandleInput();
        }

        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    private void HandleInput()
    {
        HandleEsc();
        HandleHorizontalMovement();
        HandleJump();
        HandleInteraction();
        
        _parallaxBackground.Move(rb.velocity.x);
    }

    private void HandleEsc()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameManager.ResetLevel();
        }
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

    public override void OnLevelReset()
    {
        CurrentState = State.DEFAULT;
        CarriedBlock = null;
    }
}
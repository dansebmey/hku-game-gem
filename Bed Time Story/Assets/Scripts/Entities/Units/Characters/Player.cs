using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    #region Attributes
    
    [SerializeField] private ParallaxBackground parallaxBackground;
    [SerializeField] private Transform respawnBook;
    
    public float defaultJumpForce;
    private float _jumpForce;

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
            }
        }
    }

    private Vector3 _cachedVelocityOnPause;

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
        HandlePause();
        if (GameManager.gameState == GameManager.GameState.PLAYING)
        {
            HandleInput();
        }

        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    private void HandleInput()
    {
        HandleHorizontalMovement();
        HandleJump();
        HandleInteraction();

        parallaxBackground.Move(rb.velocity.x);
    }

    private void HandlePause()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            switch (GameManager.gameState)
            {
                case GameManager.GameState.PLAYING:
                    GameManager.TogglePause(true);
                    break;
                case GameManager.GameState.PAUSED:
                    GameManager.TogglePause(false);
                    break;
            }
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            if (GameManager.gameState == GameManager.GameState.PAUSED)
            {
                GameManager.TogglePause(false);
            }
            GameManager.ResetToCheckpoint();
        }
    }

    private void HandleHorizontalMovement()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xSpeed * moveSpeed, rb.velocity.y);

        animator.SetFloat("HorizontalSpeed", rb.velocity.x);

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
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            GameManager.AudioManager.Play("Jump", 0.1f);
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
                ixArea.objectInFocus?.OnInteract(this);
            }
            else
            {
                CarriedBlock.OnInteract(this);
            }
        }
    }

    private bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }

    public override void OnLevelReset()
    {
        CurrentState = State.DEFAULT;
        CarriedBlock = null;
    }

    public override void TogglePause(bool doPause)
    {
        base.TogglePause(doPause);
        
        if (doPause)
        {
            _cachedVelocityOnPause = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
        }
        else
        {
            rb.velocity = _cachedVelocityOnPause;
            rb.gravityScale = 2;
        }
    }
}
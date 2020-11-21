using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Unit
{
    public float jumpForce;
    
    protected bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    protected List<System.Type> interactableObjectTypes;

    protected IXArea ixArea;

    protected override void Awake()
    {
        base.Awake();
        
        ixArea = GetComponentInChildren<IXArea>();
    }

    protected override void Start()
    {
        base.Start();
        
        interactableObjectTypes = new List<Type>();
        InitInteractableObjectTypes();
    }

    protected abstract void InitInteractableObjectTypes();

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
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (xSpeed < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
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
            ixArea.propInFocus?.OnInteract(this);
        }
    }
}
using System;
using UnityEngine;

public abstract class Entity : SWObject
{
    protected Animator animator;
    
    public float defaultMoveSpeed;
    protected float moveSpeed;
    
    protected Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        GameManager.RegisterEntity(this);
        
        moveSpeed = defaultMoveSpeed;
    }

    public virtual void TogglePause(bool doPause)
    {
        
    }
}
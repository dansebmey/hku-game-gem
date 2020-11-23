using System;
using UnityEngine;

public abstract class Unit : SWObject
{
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
        
        moveSpeed = defaultMoveSpeed;
    }
}
using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public float moveSpeed;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
}
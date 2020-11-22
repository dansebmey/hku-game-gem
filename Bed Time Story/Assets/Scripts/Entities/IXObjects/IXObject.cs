using System;
using UnityEngine;

public abstract class IXObject : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D[] coll2ds;
    
    protected Transform tooltip;
    protected bool isBeingCarried;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll2ds = GetComponents<Collider2D>();
    }

    public virtual void OnPickup(Player actor)
    {
        isBeingCarried = true;
    }

    public virtual void OnDrop(Player actor)
    {
        isBeingCarried = false;
    }

    protected virtual void Update()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    public void OnEnterScope()
    {
        tooltip.gameObject.SetActive(true);
    }
    
    public void OnLeaveScope()
    {
        tooltip.gameObject.SetActive(false);
    }
}
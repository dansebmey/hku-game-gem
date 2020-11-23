using System;
using UnityEngine;

public abstract class IXObject : SWObject
{
    protected Rigidbody2D rb;
    protected Collider2D[] coll2ds;
    
    protected Transform tooltip;

    private static bool _showTooltip = true;
    public bool ShowTooltip
    {
        get => _showTooltip;
        set => _showTooltip = value;
    }

    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        coll2ds = GetComponents<Collider2D>();
    }

    public abstract void OnPickup(Player actor);

    public abstract void OnDrop(Player actor);

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
        tooltip.gameObject.SetActive(_showTooltip);
    }
    
    public void OnLeaveScope()
    {
        tooltip.gameObject.SetActive(false);
    }
}
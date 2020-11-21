using System;
using UnityEngine;

public abstract class IXObject : MonoBehaviour
{
    protected Collider2D coll2d;
    protected SpriteRenderer ixSpriteRenderer;
    protected bool isBeingCarried;

    protected virtual void Awake()
    {
        coll2d = GetComponent<Collider2D>();
    }

    public abstract void OnPickup(Player actor);
    public abstract void OnDrop(Player actor);

    protected virtual void Update()
    {
        
    }

    public void OnEnterScope()
    {
        ixSpriteRenderer.enabled = true;
    }
    
    public void OnLeaveScope()
    {
        ixSpriteRenderer.enabled = false;
    }
}
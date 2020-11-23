using UnityEngine;

public abstract class PIXObject : IXObject
{
    protected Rigidbody2D rb;
    protected Collider2D[] coll2ds;

    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        coll2ds = GetComponents<Collider2D>();
    }
}
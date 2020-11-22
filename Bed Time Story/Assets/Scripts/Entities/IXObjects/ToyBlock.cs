using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBlock : IXObject
{
    // private SpriteRenderer _mirroredOutlineSpriteRenderer;
    private SpriteRenderer _spriteRenderer;
    public Sprite sprite2d, sprite3d;

    protected override void Awake()
    {
        base.Awake();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foreach (Transform tf in GetComponentsInChildren<Transform>(true))
        {
            if (tf.CompareTag("Meta"))
            {
                tooltip = tf;
            }
        }
        // _mirroredOutlineSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[2];
    }

    public override void OnPickup(Player actor)
    {
        base.OnPickup(actor);
        
        _spriteRenderer.sprite = sprite3d;
        foreach (var c2d in coll2ds)
        {
            c2d.enabled = false;   
        }
        actor.CarriedBlock = this;
        isBeingCarried = true;
        // reset and freeze rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;
        
        transform.parent = actor.transform;
        var actorScale = actor.transform.localScale;
        transform.localScale = new Vector3(actorScale.x * -1, actorScale.y);
        
        // _mirroredOutlineSpriteRenderer.enabled = true;
    }

    public override void OnDrop(Player actor)
    {
        base.OnDrop(actor);
        
        _spriteRenderer.sprite = sprite2d;
        foreach (var c2d in coll2ds)
        {
            c2d.enabled = true;   
        }
        actor.CarriedBlock = null;
        isBeingCarried = false;
        rb.freezeRotation = false;

        var tf = transform;
        var actorScale = actor.transform.localScale;
        tf.localScale = actorScale;
        tf.position += new Vector3(actorScale.x * 0.5f, 0);
        
        tf.parent = null;

        // _mirroredOutlineSpriteRenderer.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (isBeingCarried)
        {
            transform.localPosition = new Vector3(0.5f, -0.5f);

            // _mirroredOutlineSpriteRenderer.transform.localPosition = new Vector3(transform.localScale.normalized.x * 0.5f, 0);
        }
    }
}
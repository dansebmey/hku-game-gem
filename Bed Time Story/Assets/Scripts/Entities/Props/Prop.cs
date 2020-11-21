using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour
{
    protected Collider2D coll2d;
    
    private Vector3 _targetPos;
    private SpriteRenderer _ixSpriteRenderer;
    private SpriteRenderer _mirroredOutlineSpriteRenderer;
    
    private bool _highlighted;
    private bool _isBeingCarried;

    private void Awake()
    {
        coll2d = GetComponent<Collider2D>();
        
        _ixSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[1];
        _mirroredOutlineSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[2];
    }

    public virtual void OnPickup(Character actor)
    {
        _isBeingCarried = true;
        
        actor.CarriedItem = this;
        actor.CurrentState = Character.State.CARRYING_AN_ITEM;

        coll2d.enabled = false;
        transform.parent = actor.transform;
        
        var actorScale = actor.transform.localScale;
        transform.localScale = new Vector3(actorScale.x * -1, actorScale.y);
        
        _mirroredOutlineSpriteRenderer.enabled = true;
    }

    public void OnDrop(Character actor)
    {
        _isBeingCarried = false;
        
        actor.CarriedItem = null;
        actor.CurrentState = Character.State.DEFAULT;
        transform.localScale = actor.transform.localScale;

        coll2d.enabled = true;
        transform.parent = null;
        
        _mirroredOutlineSpriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (_isBeingCarried)
        {
            transform.localPosition = new Vector3(0.5f, -0.5f);
        }
    }

    private IEnumerator MoveToTargetPos()
    {
        while (_targetPos != transform.position)
        {
            Vector3 pos = transform.position;
            transform.position = Vector3.Lerp(pos, _targetPos, 0.1f);
            
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnEnterScope()
    {
        _highlighted = true;
        
        _ixSpriteRenderer.enabled = true;
    }
    
    public void OnLeaveScope()
    {
        _highlighted = false;
        
        _ixSpriteRenderer.enabled = false;
    }
}

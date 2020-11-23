using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBlock : IXObject
{
    private enum State { DEFAULT, CARRIED }

    private State _currentState;
    private State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (value)
            {
                case State.DEFAULT:
                    _spriteRenderer.sprite = sprite2d;
                    foreach (var c2d in coll2ds)
                    {
                        c2d.enabled = true;   
                    }
                    rb.freezeRotation = false;
                    transform.parent = null;
                    
                    break;
                case State.CARRIED:
                    _spriteRenderer.sprite = sprite3d;
                    foreach (var c2d in coll2ds)
                    {
                        c2d.enabled = false;   
                    }
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rb.freezeRotation = true;
                    
                    break;
            }
        }
    }
    
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
    }

    public override void OnPickup(Player actor)
    {
        CurrentState = State.CARRIED;
        actor.CarriedBlock = this;
        
        transform.parent = actor.transform;
        var actorScale = actor.transform.localScale;
        transform.localScale = new Vector3(actorScale.x * -1, actorScale.y);
        
    }

    public override void OnDrop(Player actor)
    {
        CurrentState = State.DEFAULT;
        actor.CarriedBlock = null;

        var actorScale = actor.transform.localScale;
        transform.localScale = actorScale;
        transform.position += new Vector3(actorScale.x * 0.5f, 0);
    }

    protected override void Update()
    {
        base.Update();
        
        if (_currentState == State.CARRIED)
        {
            transform.localPosition = new Vector3(0.5f, -0.5f);
        }
    }

    public override void OnLevelReset()
    {
        CurrentState = State.DEFAULT;
    }
}
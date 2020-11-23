using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBlock : PIXObject
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
                    spriteRenderer.sprite = sprite2d;
                    foreach (var c2d in coll2ds)
                    {
                        c2d.enabled = true;   
                    }
                    rb.freezeRotation = false;
                    transform.parent = null;
                    
                    break;
                case State.CARRIED:
                    spriteRenderer.sprite = sprite3d;
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
    
    public Sprite sprite2d, sprite3d;

    protected override void _OnInteract(Player actor)
    {
        var actorScale = actor.transform.localScale;
        switch (CurrentState)
        {
            case State.DEFAULT:
                CurrentState = State.CARRIED;
                actor.CarriedBlock = this;
        
                transform.parent = actor.transform;
                transform.localScale = new Vector3(actorScale.x * -1, actorScale.y);
                break;
            case State.CARRIED:
                CurrentState = State.DEFAULT;
                actor.CarriedBlock = null;

                transform.localScale = actorScale;
                transform.position += new Vector3(actorScale.x * 0.5f, 0);
                break;
        }
        
    }

    protected void Update()
    {
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Child : Entity
{
    public enum State { DEFAULT, CRYING }
    private State _currentState = State.DEFAULT;
    public State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case State.DEFAULT:
                    animator.SetBool("IsCrying", false);
                    animator.Play("ChildWalk");
                    moveSpeed = defaultMoveSpeed;
                    break;
                case State.CRYING:
                    animator.SetBool("IsCrying", true);
                    moveSpeed = 0;
                    GameManager.ResetToCheckpoint();
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (GameManager.gameState == GameManager.GameState.PLAYING)
        {
            transform.position += Vector3.right * (moveSpeed * 0.01f);   
        }
    }

    public override void OnLevelReset()
    {
        CurrentState = State.DEFAULT;
    }
}

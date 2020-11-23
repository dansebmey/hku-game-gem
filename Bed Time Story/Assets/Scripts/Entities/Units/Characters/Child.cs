using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Child : Unit
{
    private Animator _animator;

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
                    moveSpeed = defaultMoveSpeed;
                    break;
                case State.CRYING:
                    _animator.SetBool("IsCrying", true);
                    moveSpeed = 0;
                    GameManager.ResetLevel();
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        _animator = GetComponentInChildren<Animator>();
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Child : Unit
{
    private Animator animator;

    protected override void Update()
    {
        base.Update();
        transform.position += Vector3.right * (moveSpeed * 0.01f);
    }

    public void Cry()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("IsCrying", true);
        moveSpeed = 0;
    }
}

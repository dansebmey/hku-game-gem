using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Unit
{
    protected override void Update()
    {
        base.Update();
        transform.position += Vector3.right * (moveSpeed * 0.01f);
    }

    public void Cry()
    {
        moveSpeed = 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour
{
    private SpriteRenderer ixSpriteRenderer;

    private void Awake()
    {
        ixSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[1];
    }

    public abstract void OnInteract(Character actor);

    public void OnEnterScope()
    {
        ixSpriteRenderer.enabled = true;
    }
    
    public void OnLeaveScope()
    {
        ixSpriteRenderer.enabled = false;
    }
}

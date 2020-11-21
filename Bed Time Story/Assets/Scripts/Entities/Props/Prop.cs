using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour
{
    private Vector3 targetPos;
    private SpriteRenderer _ixSpriteRenderer;

    private void Awake()
    {
        _ixSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[1];
    }

    public virtual void OnInteract(Character actor)
    {
        Vector3 pos = transform.position;
        
        targetPos = new Vector3(pos.x, pos.y * -1);
        StartCoroutine(MoveToTargetPos());
    }

    private IEnumerator MoveToTargetPos()
    {
        while (targetPos != transform.position)
        {
            yield return new WaitForSeconds(1.0f/60);
        }
    }

    public void OnEnterScope()
    {
        _ixSpriteRenderer.enabled = true;
    }
    
    public void OnLeaveScope()
    {
        _ixSpriteRenderer.enabled = false;
    }
}

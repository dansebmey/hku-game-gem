using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour
{
    private Vector3 _targetPos;
    private SpriteRenderer _ixSpriteRenderer;
    private SpriteRenderer _mirroredOutlineSpriteRenderer;
    
    private bool _highlighted;

    private void Awake()
    {
        _ixSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[1];
        _mirroredOutlineSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[2];
    }

    public virtual void OnInteract(Character actor)
    {
        Vector3 pos = transform.position;
        
        _targetPos = new Vector3(pos.x, pos.y * -1);
        StartCoroutine(MoveToTargetPos());
    }

    private void Update()
    {
        if (_highlighted)
        {
            Vector3 pos = transform.position;
            _mirroredOutlineSpriteRenderer.transform.position = new Vector3(pos.x, pos.y * -1);
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
        
        // _ixSpriteRenderer.enabled = true;
        _mirroredOutlineSpriteRenderer.enabled = true;
    }
    
    public void OnLeaveScope()
    {
        _highlighted = false;
        
        // _ixSpriteRenderer.enabled = false;
        _mirroredOutlineSpriteRenderer.enabled = false;
    }
}

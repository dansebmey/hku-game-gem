using System;
using UnityEngine;

public class HoleScanArea : TriggerArea
{
    private Child _child;

    private void Awake()
    {
        _child = GetComponentInParent<Child>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var dropArea = other.gameObject.GetComponent<BlockDropArea>();
        if (dropArea != null)
        {
            if (!dropArea.containsBlock)
            {
                _child.CurrentState = Child.State.CRYING;
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
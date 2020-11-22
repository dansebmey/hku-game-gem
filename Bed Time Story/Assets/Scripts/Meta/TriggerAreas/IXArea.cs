using System;
using UnityEngine;

public class IXArea : PlayerArea
{
    internal ToyBlock propInFocus;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<ToyBlock>();
        if (prop != null && propInFocus == null)
        {
            propInFocus = prop;
            propInFocus.OnEnterScope();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        propInFocus?.OnLeaveScope();
        propInFocus = null;
    }
}
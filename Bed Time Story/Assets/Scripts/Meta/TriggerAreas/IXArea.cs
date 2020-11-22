using System;
using UnityEngine;

public class IXArea : PlayerArea
{
    internal ToyBlock propInFocus;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<ToyBlock>();
        if (prop != null && propInFocus == null)
        {
            propInFocus = prop;
            propInFocus.OnEnterScope();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        propInFocus?.OnLeaveScope();
        propInFocus = null;
    }
}
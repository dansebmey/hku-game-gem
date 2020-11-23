using System;
using UnityEngine;

public class IXArea : PlayerArea
{
    internal IXObject objectInFocus;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<IXObject>();
        if (prop != null && objectInFocus == null) // objectInFocus == null prevents multiple objects from being targeted at the same time
        {
            objectInFocus = prop;
            objectInFocus.OnEnterScope();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        objectInFocus?.OnLeaveScope();
        objectInFocus = null;
    }
}
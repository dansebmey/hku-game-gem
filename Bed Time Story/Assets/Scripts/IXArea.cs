using System;
using UnityEngine;

public class IXArea : MonoBehaviour
{
    private Character _observer;
    internal Prop propInFocus;

    private void Awake()
    {
        _observer = GetComponentInParent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<Prop>();
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
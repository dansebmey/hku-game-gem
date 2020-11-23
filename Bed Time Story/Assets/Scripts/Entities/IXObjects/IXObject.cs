using System;
using UnityEngine;

public abstract class IXObject : SWObject
{
    protected Transform tooltip;

    protected bool isInteractable = true;

    protected SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foreach (Transform tf in GetComponentsInChildren<Transform>(true))
        {
            if (tf.CompareTag("Meta"))
            {
                tooltip = tf;
            }
        }
    }

    public void OnInteract(Player actor)
    {
        if (isInteractable)
        {
            _OnInteract(actor);
            GameManager.RegisterTooltipShown(GetType());
        }
    }

    protected abstract void _OnInteract(Player actor);

    public void OnEnterScope()
    {
        tooltip.gameObject.SetActive(!GameManager.IsTooltipShownFor(GetType()));
    }
    
    public void OnLeaveScope()
    {
        tooltip.gameObject.SetActive(false);
    }
}
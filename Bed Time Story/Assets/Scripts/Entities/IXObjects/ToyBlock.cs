using System;
using UnityEngine;

public class ToyBlock : IXObject
{
    private SpriteRenderer _mirroredOutlineSpriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        
        ixSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[1];
        _mirroredOutlineSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true)[2];
    }

    public override void OnPickup(Player actor)
    {
        isBeingCarried = true;
        
        actor.CarriedItem = this;
        actor.CurrentState = Player.State.CARRYING_AN_ITEM;

        coll2d.enabled = false;
        transform.parent = actor.transform;
        
        var actorScale = actor.transform.localScale;
        transform.localScale = new Vector3(actorScale.x * -1, actorScale.y);
        
        _mirroredOutlineSpriteRenderer.enabled = true;
    }

    public override void OnDrop(Player actor)
    {
        isBeingCarried = false;
        
        actor.CarriedItem = null;
        actor.CurrentState = Player.State.DEFAULT;
        transform.localScale = actor.transform.localScale;

        coll2d.enabled = true;
        transform.parent = null;
        
        _mirroredOutlineSpriteRenderer.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (isBeingCarried)
        {
            transform.localPosition = new Vector3(0.5f, -0.5f);

            _mirroredOutlineSpriteRenderer.transform.position
                = transform.InverseTransformPoint(0, -0.5f, 0);
        }
    }
}
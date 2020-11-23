using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBook : IXObject
{
    public override void OnLevelReset()
    {
        
    }

    protected override void _OnInteract(Player actor)
    {
        GameManager.SetCheckpoint();
        isInteractable = false;
        OnLeaveScope();
        spriteRenderer.color = Color.white;
    }
}

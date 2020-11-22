using UnityEngine;

public abstract class PlayerArea : TriggerArea
{
    protected Player Player;

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
    }
}
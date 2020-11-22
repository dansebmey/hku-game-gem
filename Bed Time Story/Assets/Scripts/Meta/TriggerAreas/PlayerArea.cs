using UnityEngine;

public class PlayerArea : TriggerArea
{
    protected Player Player;

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
    }
}
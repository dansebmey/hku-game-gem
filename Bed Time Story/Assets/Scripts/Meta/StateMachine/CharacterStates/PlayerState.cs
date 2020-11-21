using UnityEngine;

public abstract class PlayerState : BaseState
{
    protected Character character;
    
    public override void Init(FSM fsm)
    {
        base.Init(fsm);
        
        character = GetComponent<Character>();
    }
}
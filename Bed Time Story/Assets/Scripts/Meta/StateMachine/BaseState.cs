using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected FSM fsm;

    public void Init(FSM fsm)
    {
        this.fsm = fsm;
    }
    
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
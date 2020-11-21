using System;
using System.Collections.Generic;

public class FSM
{
    private Dictionary<Type, BaseState> stateDictionary;
    private BaseState currentState;

    public FSM(params BaseState[] states)
    {
        stateDictionary = new Dictionary<Type, BaseState>();

        foreach (BaseState state in states)
        {
            if (state != null)
            {
                state.Init(this);
                stateDictionary.Add(state.GetType(), state);
            }
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type newStateType)
    {
        currentState?.OnExit();
        currentState = stateDictionary[newStateType];
        currentState?.OnEnter();
    }
}
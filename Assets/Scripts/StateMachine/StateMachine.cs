using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public IState curState;

    public void ChangeState(IState newState)
    {
        curState?.Exit();
        curState = newState;
        curState.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }
}

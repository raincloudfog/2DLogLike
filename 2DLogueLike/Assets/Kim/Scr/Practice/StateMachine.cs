using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState curState { get; private set; }

    public StateMachine(IState defaultState)
    {
        curState = defaultState;
    }

    public void SetState(IState state)
    {
        if(curState == state)
        {
            Debug.Log("현재 이미 해당 상태");
            return;
        }

        curState.OperateEnter();

        curState = state;

        curState.OperateEnter();
    }
    public void DoOperateUpdate()
    {
        curState.OperateUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEenemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Attack,
        Rush,
    }
    StateMachine stateMachine;
    Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();
    void Start()
    {
        IState idle = new StateIdle();

        dicState.Add(EnemyState.Idle, idle);

        stateMachine = new StateMachine(idle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

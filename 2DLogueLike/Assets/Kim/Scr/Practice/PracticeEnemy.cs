using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeEnemy : MonoBehaviour
{
    Dictionary<EnemyState, IEnemyState> stateDic;
    IEnemyState curState;
    void Awake()
    {
        stateDic = new Dictionary<EnemyState, IEnemyState>
        {
            { EnemyState.Idle, new IdleState() },
            { EnemyState.Move, new MoveState() },
            { EnemyState.Attack, new AttackState()},
            { EnemyState.Die, new DieState() },
            // 다른 상태 클래스들도 추가합니다.
        };

        // 초기 상태 설정
        curState = stateDic[EnemyState.Idle];
        curState.EnterState();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Die
}
public interface IEnemyState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}

public class IdleState : IEnemyState
{
    public void EnterState()
    {
        // Idle상태에 진입할 떄 실행되는 동작
    }
    public void UpdateState()
    {
        // Idle상태에서 매 프레임 실행되는 동작
    }
    public void ExitState()
    {
        // Idle상태에서 벗어날 때 실해오디는 동작
    }

}
public class MoveState : IEnemyState
{
    public void EnterState()
    {
        // Move 상태에 진입할 때 실행되는 동작을 구현합니다.
    }

    public void UpdateState()
    {
        // Move 상태에서 매 프레임 실행되는 동작을 구현합니다.
    }

    public void ExitState()
    {
        // Move 상태에서 벗어날 때 실행되는 동작을 구현합니다.
    }
}

public class AttackState : IEnemyState
{
    public void EnterState()
    {
        // Move 상태에 진입할 때 실행되는 동작을 구현합니다.
    }

    public void UpdateState()
    {
        // Move 상태에서 매 프레임 실행되는 동작을 구현합니다.
    }

    public void ExitState()
    {
        // Move 상태에서 벗어날 때 실행되는 동작을 구현합니다.
    }
}

public class DieState : IEnemyState
{
    public void EnterState()
    {
        // Move 상태에 진입할 때 실행되는 동작을 구현합니다.
    }

    public void UpdateState()
    {
        // Move 상태에서 매 프레임 실행되는 동작을 구현합니다.
    }

    public void ExitState()
    {
        // Move 상태에서 벗어날 때 실행되는 동작을 구현합니다.
    }
}



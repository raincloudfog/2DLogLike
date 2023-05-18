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
        // Idle���¿� ������ �� ����Ǵ� ����
    }
    public void UpdateState()
    {
        // Idle���¿��� �� ������ ����Ǵ� ����
    }
    public void ExitState()
    {
        // Idle���¿��� ��� �� ���ؿ���� ����
    }

}
public class MoveState : IEnemyState
{
    public void EnterState()
    {
        // Move ���¿� ������ �� ����Ǵ� ������ �����մϴ�.
    }

    public void UpdateState()
    {
        // Move ���¿��� �� ������ ����Ǵ� ������ �����մϴ�.
    }

    public void ExitState()
    {
        // Move ���¿��� ��� �� ����Ǵ� ������ �����մϴ�.
    }
}

public class AttackState : IEnemyState
{
    public void EnterState()
    {
        // Move ���¿� ������ �� ����Ǵ� ������ �����մϴ�.
    }

    public void UpdateState()
    {
        // Move ���¿��� �� ������ ����Ǵ� ������ �����մϴ�.
    }

    public void ExitState()
    {
        // Move ���¿��� ��� �� ����Ǵ� ������ �����մϴ�.
    }
}

public class DieState : IEnemyState
{
    public void EnterState()
    {
        // Move ���¿� ������ �� ����Ǵ� ������ �����մϴ�.
    }

    public void UpdateState()
    {
        // Move ���¿��� �� ������ ����Ǵ� ������ �����մϴ�.
    }

    public void ExitState()
    {
        // Move ���¿��� ��� �� ����Ǵ� ������ �����մϴ�.
    }
}



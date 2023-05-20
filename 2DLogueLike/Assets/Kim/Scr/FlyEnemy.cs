using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    Vector2 saveOffset;
    private void Awake()
    {
        Init();
    
    }

    private void OnEnable()
    {
        Init();
    }

    void Update()
    {
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
    }
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);
        StateEnemyPatton();
        Die();
    }

    void StateEnemyPatton()
    {
        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Attack:
                if (startCo == true) // �ڷ�ƾ�� �ѹ��� �����Ű�� ���� �Ұ�
                {
                    startCo = false;
                    StartCoroutine(IAttack());
                }
                break;
        }
    }

    void Idle() // �⺻���� -> �������� �÷��̾ ������ �÷��̾ �m�ư��� ����
    {
        anim.SetBool("isRush", false);
        EnemyFilp(rigid.velocity.x);
        if (col == null && isDie == false)
        {
            rigid.velocity = offset.normalized * curEnemySpeed;
        }
   
        else if (col != null && isDie == false)
        {
            SetState(State.Attack);
        }

    }
    IEnumerator IAttack() // �������� �÷��̾ ������ �����ϴ� ����
    {
        anim.SetBool("isRush", true);
        startCo = false;
        AttackEnemyFilp();
        saveOffset = offset;
        rigid.velocity = Vector2.zero;
        rigid.velocity = -saveOffset.normalized * 4;
        PlaySound("isShot");
        yield return new WaitForSeconds(0.3f);
        rigid.velocity = saveOffset.normalized * 10;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isRush", false);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        startCo = true;
        SetState(State.Idle);
    }

    void AttackEnemyFilp()
    {
        localScale = transform.localScale;
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1;
        transform.localScale = localScale;
    }
    void EnemyFilp(float x)
    {
        localScale = transform.localScale;
        if (x != 0)
        {
            localScale.x = x < 0 ? -1: 1;
        }
        transform.localScale = localScale;
    }
    void Die()
    {
        if (isDie == true && isrealDie == false)
        {
            isrealDie = true;
            anim.SetTrigger("isDie");
            StartCoroutine(ReturnDelay());
            rigid.velocity = Vector2.zero;
            capCol.enabled = false;
        }
    }
    IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(1f);
        EnemyObjectPool.instance.enemyPool.ReturnFEnemy(this); // ������ ������Ʈ�� ���� ��Ŵ
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                Food food = EnemyObjectPool.instance.itemPool.GetFood();
                food.transform.position = transform.position;
                break;
            case 1:
                Coin coin = EnemyObjectPool.instance.itemPool.GetCoin();
                coin.transform.position = transform.position;
                break;
        }
        isrealDie = false;
    }
}

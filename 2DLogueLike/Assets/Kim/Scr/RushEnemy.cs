using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEnemy : Enemy
{
    public float timer;
    bool isPatrol = true;
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
            case State.Patrol:
                Patrol();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void Idle() // 가만히 
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        timer += Time.deltaTime;
        if(timer > Random.Range(1, 5) && isDie == false)
        {
            timer = 0;
            SetState(State.Patrol);
        }
        if (col != null && isDie == false)
        {
            SetState(State.Attack);
        }

    }

    void Patrol() // 순찰 
    {
        anim.SetBool("isMove", true);
        if (col != null && isDie == false)
        {
            SetState(State.Attack);
        }
        else if(col == null && isDie == false)
        {
            if (isPatrol == true)
            {
                isPatrol = false;
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                Debug.Log(x + "/" + y);
                rigid.velocity = new Vector2(x, y).normalized * 1.5f;
                EnemyFilp(x);
                StartCoroutine(RandomMove());
            }
        }
    }
   
    void Attack()
    {
        StopCoroutine(RandomMove());
        anim.SetBool("isMove", false);
        anim.SetBool("isRush",true);
        if(col != null && isDie == false)
        {
            AttackEnemyFilp();
            ranginPlayer = 10;
            rigid.velocity = offset.normalized * curEnemySpeed;
        }

    }
    void EnemyFilp(float x)
    {
        localScale = transform.localScale;
        if (x != 0)
        {
            localScale.x = x < 0 ? -1 : 1;
        }
        transform.localScale = localScale;
    }
    void AttackEnemyFilp()
    {
        localScale = transform.localScale;
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1;    
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
        EnemyObjectPool.instance.enemyPool.ReturnREnemy(this); // 죽으면 오브젝트를 리턴 시킴
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

    IEnumerator RandomMove()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                isPatrol = true;
                SetState(State.Idle);
                break;
            case 1:
                isPatrol = true;
                break;
        }
    }
}

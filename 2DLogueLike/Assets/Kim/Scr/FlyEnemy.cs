using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    //Character player;
    //GameObject player;
    Collider2D bodyCol = new Collider2D();
    CapsuleCollider2D capCol;
    SpriteRenderer spren; // 색상이 변할 스프라이트 렌더러 컴포넌트
    public Color targetColor; // 목표 색상
    public float duration = 2f; // 전체 애니메이션 시간 (초)
    public float timer;
    Vector2 offset;
    bool isPatrol = true;
    private void Awake()
    {
        Init();
        spren = GetComponent<SpriteRenderer>();
    }
    protected override void Init()
    {
        base.Init();
        if (capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
        }
    }
    private void OnEnable()
    {
        Init();
        capCol.enabled = true;
        curEnemyHp = enemyHp;
        curEnemySpeed = enemySpeed;
        curBodyDamage = bodyDamage;
        //player = EnemyObjectPool.instance.testPlayer;
    }

    void Update()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
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

        Die();
    }

    void Idle() // 가만히 
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        timer += Time.deltaTime;
        if (timer > Random.Range(1, 5) && isDie == false)
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
        else if (col == null && isDie == false)
        {
            if (isPatrol == true)
            {
                EnemyFilp();
                isPatrol = false;
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                Debug.Log(x + "/" + y);
                rigid.velocity = new Vector2(x, y).normalized * 1.5f;
                StartCoroutine(RandomMove());
            }

        }
    }

    void Attack()
    {
        StopCoroutine(RandomMove());
        anim.SetBool("isMove", false);
        anim.SetBool("isRush", true);
        if (col != null && isDie == false)
        {
            offset = EnemyObjectPool.instance.player.transform.position - transform.position;
            rigid.velocity = offset.normalized * curEnemySpeed;
            //StartCoroutine(ChangeColorOverTime()); 
        }

    }
    void EnemyFilp()
    {
        Vector3 localScale = transform.localScale;
        if (transform.position.x != 0)
        {
            if (rigid.velocity.x < 0)
            {
                localScale.x = -1;
            }
            else
            {
                localScale.x = 1;
            }
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
        //EnemyObjectPool.instance.enemyPool.ReturnREnemy(this); // 죽으면 오브젝트를 리턴 시킴
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

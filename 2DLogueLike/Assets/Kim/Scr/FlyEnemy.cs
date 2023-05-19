using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    enum LayerData // 레이어 데이터
    {
        Player = 6,
        Wall = 8
    }
    //Character player;
    //GameObject player;
    CapsuleCollider2D capCol;
    Collider2D wallcheckCol = new Collider2D();
    SpriteRenderer spren; // 색상이 변할 스프라이트 렌더러 컴포넌트
    public Color targetColor; // 목표 색상
    public float duration = 2f; // 전체 애니메이션 시간 (초)
    public float timer;
    public float ranginWall;

    Vector2 offset;
    Vector2 saveOffset;
    Vector2 patrolVec;
    Vector3 localScale;
    public LayerMask wallLayer;
    public Transform wallCheck;
    bool isPatrol = true;
    bool isRush = false;
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
   
            case State.Attack:
                if (startCo == true) // 코루틴을 한번만 실행시키기 위한 불값
                {
                    startCo = false; 
                    //Attack();
                    
                    StartCoroutine(IAttack());
                }
                break;
        }
        Die();
    }

    void Idle() // 기본상태 -> 범위내에 플레이어가 없으면 플레이어를 쫗아가는 상태
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
    IEnumerator IAttack() // 범위내에 플레이어가 있으면 돌진하는 패턴
    {
        
        anim.SetBool("isRush", true);
        startCo = false;
        AttackEnemyFilp();
        Vector2 saveOffset = offset;
        rigid.velocity = Vector2.zero;
        rigid.velocity = -saveOffset.normalized * 4;
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
        if (EnemyObjectPool.instance.player.transform.position.x < transform.position.x)
        {
            localScale.x = -1f;
        }
        else
        {
            localScale.x = 1f;
        }
        transform.localScale = localScale;
    }
    void EnemyFilp(float x)
    {
        Vector3 localScale = transform.localScale;
        if (x != 0)
        {
            if (x < 0)
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
        EnemyObjectPool.instance.enemyPool.ReturnFEnemy(this); // 죽으면 오브젝트를 리턴 시킴
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

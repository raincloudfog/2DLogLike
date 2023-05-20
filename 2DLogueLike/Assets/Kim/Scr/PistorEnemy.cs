using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistorEnemy : Enemy
{
    Collider2D col2 = new Collider2D();
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
        StateEnemyPatton();
    }

    void StateEnemyPatton()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Attack:
                Attack();
                break;
        }
        Die();
    }

    void Idle()
    {
        anim.SetBool("isMove", false);
        if (col != null && isDie == false)
        {
            SetState(State.Attack);
        }
    }

    void Attack()
    {
        col2 = Physics2D.OverlapCircle(transform.position, ranginShot, playerLayer);
        anim.SetBool("isMove", col);
       
        if (col == null && isDie == false)
        {
            SetState(State.Idle);
            rigid.velocity = Vector2.zero;
        }
        else if (col != null && col2 == null && isDie == false)
        {
            rigid.velocity = offset.normalized * curEnemySpeed;
            EnemyFilp();
        }


        else if (col2 != null && col != null && isDie == false)
        {
            anim.SetBool("isMove", false);
            EnemyFilp();
            rigid.velocity = Vector2.zero;
            if (isAttack)
            {
                isAttack = false;
                Pistor();
                
                StartCoroutine(Delay());
            }
        }
    }

    void EnemyFilp()
    {
        localScale = transform.localScale; 
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1;       
        transform.localScale = localScale;
    }
    void Pistor()
    {
        Enemybullet obj = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
        obj.transform.position = transform.position;
        obj.SetRigidBullet(offset, curEnemyBulletSpeed);
        PlaySound("isShot");
    }
    void Die() // 죽었을 때의 함수
    {
        if (isDie == true && isrealDie == false) // 한번만 실행하기 위함
        {
            isrealDie = true;
            anim.SetTrigger("isDie");
            rigid.velocity = Vector2.zero;
            StartCoroutine(ReturnDelay());
            capCol.enabled = false;
        }
    }

    IEnumerator ReturnDelay() // 적들을 오브젝트 풀로 다시 돌려주는 코루틴
    {
        yield return new WaitForSeconds(1f); 
        EnemyObjectPool.instance.enemyPool.ReturnPtEnemy(this);
       
        int random = Random.Range(0,  2);
        switch (random) // 죽었을 때 랜덤으로 아이템을 드랍함
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
        isrealDie = false; // Die함수가 다시 실행될수 있도록 설정
    }
    
    IEnumerator Delay() // 적들의 공격 딜레이
    {
        yield return new WaitForSeconds(shotDelay);
        isAttack = true;
    }
}

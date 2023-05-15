using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistorEnemy : Enemy
{
    CapsuleCollider2D capCol;
    private void Awake()
    {
        Init();
        
    }
    private void OnEnable()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        if (capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
        }
        capCol.enabled = true;
        curEnemyHp = enemyHp;
        curEnemySpeed = enemySpeed;
        curEnemyBulletSpeed = enemyBulletSpeed;
        curEnemyBulletDamage = enemybulletDamage;
    }

    void Update()
    {
        StateEnemyPatton();
    }

    void StateEnemyPatton()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);

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
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, ranginShot, playerLayer);
        anim.SetBool("isMove", col);
        EnemyFilp();
        if (col == null)
        {
            SetState(State.Idle);
            rigid.velocity = Vector2.zero;
        }
        else if (col != null && col2 == null && isDie == false)
        {
            //anim.SetBool("isMove", true);
            //Vector2 offset = player.transform.position - transform.position;
            Vector2 offset = EnemyObjectPool.instance.player.transform.position - transform.position;
            rigid.velocity = offset.normalized * curEnemySpeed;
            
        }


        else if (col2 != null && col != null && isDie == false)
        {
            anim.SetBool("isMove", false);
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
        Vector3 localScale = transform.localScale;
        //player.transform.position.x
        if (EnemyObjectPool.instance.player.transform.position.x < transform.position.x)
        {
            localScale.x = -1;
        }
        else
        {
            localScale.x = 1;
        }
        transform.localScale = localScale;
    }
    void Pistor()
    {
        //Vector2 offset = player.transform.position - transform.position;
        Vector2 offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        Enemybullet obj = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
        obj.transform.position = transform.position;
        obj.SetRigidBullet(offset, curEnemyBulletSpeed, curEnemyBulletDamage);
    }
    void Die()
    {
        if (isDie == true && isrealDie == false)
        {
            isrealDie = true;
            anim.SetTrigger("isDie");
            rigid.velocity = Vector2.zero;
            StartCoroutine(ReturnDelay());
            capCol.enabled = false;
        }
    }

    IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(1f);
        EnemyObjectPool.instance.enemyPool.ReturnPtEnemy(this);
        int random = Random.Range(0,  2);
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
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(shotDelay);
        isAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossPatton
    {
        RandomFire,
        Fires,
        AroundFire,
    }
    public BossPatton curBossPatton = BossPatton.Fires;
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCol;
    Vector2 offset;

    public int bossHp;
    public int bossSpeed;
    public int bulletDamage =1;
    public int bulletSpeed = 6;
    public float timer;

    bool isShot = true;
    public bool isRandomFire = true;
    public bool isAroundFire = true;
    public bool isFires = true;

    int fireAngle = 0;

    float spinAngle = 0f;
    int count = 0;
    private void OnEnable()
    {
        Init();
    }
    void Awake()
    {
        Init();
    }
    
    // Update is called once per frame
    void Update()
    {
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        rigid.velocity = offset.normalized * bossSpeed;
        
        EnemyFilp();
        switch (curBossPatton)
        {
            case BossPatton.RandomFire:
                RandomFire();
                // 앞으로 4발 발사
                break;
            case BossPatton.Fires:
                Fires();
                // 플레이어 방향으로 샷건
                break;
            case BossPatton.AroundFire:
                AroundFire();
                // 원 형태로 전체 공격;
                break;
        }
    }
    
    void Init()
    {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if(capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
            capCol.enabled = true;
        }
        
    }

    void RandomFire() // 플레이어의 추적하면서 랜덤발사
    {
        anim.SetBool("isMove", true);
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            timer = 0;
            SetPatton();
        }
        else if (isRandomFire == true && isShot == true)
        {
            isShot = false;
            Vector2 randBullet = new Vector2(EnemyObjectPool.instance.player.transform.position.x * Random.Range(-1f, 1f),
                EnemyObjectPool.instance.player.transform.position.y * Random.Range(-1f, 1f));
            Enemybullet bullet = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBullet(randBullet, bulletSpeed, bulletDamage);
            StartCoroutine(Delay(0.2f));
        }
    }


    void Fires() // 다가가면서 상대방 위치에 조금 랜덤한 총알 발사
    {
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", true);
        if(count >= 5)
        {
            count = 0;
            SetPatton();
        }
        else if(isFires == true && isShot == true)
        {
            isShot = false;
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 2));
            dir += offset;
            Enemybullet bullet = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBullet(dir, 7, bulletDamage);
            count += 1;
            StartCoroutine(Delay(0.2f));
        }
    }
    void AroundFire() // 잠시 멈춰서 전체 공격
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        timer += Time.deltaTime;
        if(timer >= 1.5f)
        {
            timer = 0;
            anim.SetBool("isMove", false);
            anim.SetBool("isJump", true);
        }
        /*if(timer >= 2f)
        {
            timer = 0;
            anim.SetTrigger("isJump");
            int count = 30; // 총알 갯수
            float intervalAngle = 360 / count; // 총알의 각도
            float weightAngle = 0; // 각도의 차이를 주기 위해 
            for (int i = 0; i < count; ++i)
            {
                Enemybullet bullet = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                Vector2 dir = new Vector2(x, y);
                bullet.transform.position = transform.position;
                bullet.SetRigidBullet(dir, bulletSpeed, bulletDamage);
            }
            Invoke("SetPatton", 0.5f);
        }*/
        

        /*float attackDelay = 1.5f; // 공격 딜레이 
        StartCoroutine(Delay(attackDelay));
        anim.SetTrigger("isJump");
        rigid.velocity = Vector2.zero;
        if (isAroundFire == true && isShot == false)
        {
            int count = 30; // 총알 갯수
            float intervalAngle = 360 / count; // 총알의 각도
            float weightAngle = 0; // 각도의 차이를 주기 위해 
            for (int i = 0; i < count; ++ i)
            {
                Enemybullet bullet = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                Vector2 dir = new Vector2(x, y);
                bullet.transform.position = transform.position;
                bullet.SetRigidBullet(dir, bulletSpeed, bulletDamage);
            }
            //weightAngle += 1;
        }
        SetPatton();*/
    }

    void SetPatton() // 패턴을 랜덤으로 봐꿔주는 함수
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                curBossPatton = BossPatton.RandomFire;
                break;
            case 1:
                curBossPatton = BossPatton.Fires;
                break;
            case 2:
                curBossPatton = BossPatton.AroundFire;
                break;
        }
    }
    void EnemyFilp() // 싱글톤에 있는 플레이어의 위치에 따라 좌우반전시켜주는 함수
    {
        Vector3 localScale = transform.localScale;
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

    IEnumerator Delay(float seconds) // 공격 딜레이 코루틴
    {
        yield return new WaitForSeconds(seconds);
        isShot = true;

    }
    public void IsHit(int damage =1) // 보스의 히트 함수
    {
        bossHp -= damage;
        anim.SetTrigger("isHit");
        if(bossHp <= 0)
        {
            anim.SetTrigger("isDie");
            capCol.enabled = false;
            Destroy(this, 1f);
        }
    }
    void Around_Fire() // 애니메이션 점프모션이 땅바닥에 닿았을 때 실행
    {
        int count = 30; // 총알 갯수
        float intervalAngle = 360 / count; // 총알의 각도
        float weightAngle = 0; // 각도의 차이를 주기 위해 
        for (int i = 0; i < count; ++i)
        {
            Enemybullet bullet = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
            float angle = weightAngle + intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            Vector2 dir = new Vector2(x, y);
            bullet.transform.position = transform.position;
            bullet.SetRigidBullet(dir, bulletSpeed, bulletDamage);
        }
        anim.SetBool("isJump", false);
        SetPatton();
    }
}

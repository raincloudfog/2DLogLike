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
        Teleport,
        ExploreFire,
    }
    [SerializeField] ExploreBullet exploreBullet;

    public BossPatton curBossPatton = BossPatton.Fires;
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCol;
   
    Vector2 offset;
    public LayerMask layer;
    public int bossHp;
    public int bossSpeed;
    public int bulletDamage =1;
    public int bulletSpeed = 6;
    public float timer;
    public float coTimer;
    bool isShot = true;
    bool isDie = false;
    public bool isRandomFire = true;
    public bool isAroundFire = true;
    public bool isFires = true;

    int count = 0;
    bool startCo = true;
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
        Move();
    }
    void Move() // 움직임 함수 만약 보스가 죽었을 경우 리턴시켜 작동하지 않게 한다
    {
        if (isDie == true)
        {
            return;
        }
        /*offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        rigid.velocity = offset.normalized * bossSpeed;*/

        EnemyFilp();
        StateBossPatton();
    }

    void StateBossPatton() // 보스의 패턴 상태
    {
        if (startCo == true)
        {
            startCo = false;
            switch (curBossPatton)
            {
                case BossPatton.RandomFire:
                    StartCoroutine(IRandomFire());

                    //RandomFire();
                    // 앞으로 4발 발사
                    break;
                case BossPatton.Fires:
                    StartCoroutine(IFires());

                    //Fires();
                    // 플레이어 방향으로 샷건
                    break;
                case BossPatton.AroundFire:
                    AroundFire();
                    // 원 형태로 전체 공격;
                    break;
                case BossPatton.Teleport:
                    StartCoroutine(ITeleport());

                    // 순간이동
                    //Teleport();
                    break;
                case BossPatton.ExploreFire:
                    ExploreFire();
                    break;
            }
        }
    }

    void Init() // 초기값 
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
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(randBullet, bulletSpeed, bulletDamage);
            StartCoroutine(Delay(0.2f));
        }
    }
    IEnumerator IRandomFire() // 상태가 변했을 떄 한번만 실행되게 만듬
    {
        anim.SetBool("isMove", true);
        bool isStart = true;
        while (/*isShot == true*/isStart == true)
        {
            coTimer += 1f;
            if(coTimer >= 20f)
            {
                coTimer = 0;
                startCo = true;
                isStart = false;
                SetPatton();
            }
            offset = EnemyObjectPool.instance.player.transform.position - transform.position;
            rigid.velocity = offset.normalized * bossSpeed;
            Vector2 randBullet = new Vector2(EnemyObjectPool.instance.player.transform.position.x * Random.Range(-1f, 1f),
                    EnemyObjectPool.instance.player.transform.position.y * Random.Range(-1f, 1f));
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(randBullet, bulletSpeed, bulletDamage);
            yield return new WaitForSeconds(0.2f);
        }

    }
    /*IEnumerator ISetPatton(float time)
    {
        yield return new WaitForSeconds(time);
        //isShot = false;
        //startCo = true;
        SetPatton();
    }*/

    void Teleport() // 상대방의 위치로 순간이동하는 패턴
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        timer += Time.deltaTime;
        if(timer >= 3f)
        {
            timer = 0;
            anim.SetBool("isTLportOff", false);
            SetPatton();
        }
        else if(timer > 2f)
        {
            anim.SetBool("isTLportOff", true);
            anim.SetBool("isTLportOn", false);
            capCol.enabled = true;
            
        }
        else if(timer > 1.5f)
        {
            anim.SetBool("isTLportOn", true);
            capCol.enabled = false;
            
        }
    }
    IEnumerator ITeleport()
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isTLportOn", true);
        capCol.enabled = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isTLportOff", true);
        anim.SetBool("isTLportOn", false);
        capCol.enabled = true;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isTLportOff", false);
        startCo = true;
        SetPatton();
    }
   
    void Fires() // 다가가면서 상대방 위치에 조금 랜덤한 총알 발사
    {
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", false);
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
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, 7, bulletDamage);
            count += 1;
            StartCoroutine(Delay(0.2f));
        }
    }

    IEnumerator IFires()
    {
        Vector2 dir;
        anim.SetBool("isMove", false);
        while(count < 5)
        {
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 2));
            dir += offset;
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, 7, bulletDamage);
            count += 1;
            yield return new WaitForSeconds(0.2f);
        }
        count = 0;
    }
    void AroundFire() // 
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
    }
    IEnumerator IAroundFire()
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(1.5f);
        
    }
    void ExploreFire() // 폭발하는 큰 불렛을 발사한다 폭발시 AroundFire처럼 전방향으로 불렛을 뿌린다
    {
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", false);
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            timer = 0;
            SetPatton();
        }
        else if(timer > 1.58f)
        {
            anim.SetBool("isExploreFire", false);
        }
        else if(timer > 1.5f)
        {
            anim.SetBool("isExploreFire", true);
        }
    }
    
    void SetPatton() // 패턴을 랜덤으로 봐꿔주는 함수
    {
        int rand = Random.Range(0, 5);
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
            case 3:
                curBossPatton = BossPatton.Teleport;
                break;
            case 4:
                curBossPatton = BossPatton.ExploreFire;
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
            isDie = true;
            anim.SetTrigger("isDie");
            capCol.enabled = false;
            Destroy(this, 1f);
        }
    }
    void AroundFireOn() // 애니메이션 점프모션이 땅바닥에 닿았을 때 실행
    {
        int count = 30; // 총알 갯수
        float intervalAngle = 360 / count; // 총알의 각도
        float weightAngle = 0; // 각도의 차이를 주기 위해 
        for (int i = 0; i < count; ++i)
        {
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            float angle = weightAngle + intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            Vector2 dir = new Vector2(x, y);
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
        }
        anim.SetBool("isJump", false);
        SetPatton();
    }

    void TeleportOn() // 텔레포트 애니메이션에 넣을 함수
    {
        this.transform.position = EnemyObjectPool.instance.player.transform.position;
    }
    void ExploreFireOn() // 
    {
        ExploreBullet bullet = Instantiate(exploreBullet);
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        Vector2 dir = new Vector2(x, y);
        dir += offset;
        bullet.transform.position = transform.position;
        bullet.SetRigidExploreBullet(dir, bulletSpeed, bulletDamage);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerHIt(1);
        }
    }

}

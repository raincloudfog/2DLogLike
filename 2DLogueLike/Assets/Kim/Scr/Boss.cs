using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public enum BossPatton // 보스의 공격패턴
    {
        SpinFire,
        Fires,
        AroundFire,
        Teleport,
        ExploreFire,
        ShotgunFire
    }

    public enum BossAudio // 보스의 공격 사운드
    {
        Fires,
        Explore,
        TLport,
        AroundFire,
        ThreeFire,
        FiveFire
    }
    Vector2 offsetDir;

    [NonReorderable]
    public BossPatton curBossPatton = BossPatton.ExploreFire;
    public TMP_Text text;       //보스가 죽었을 때 나오는 승리문구
    public GameObject textObj; // 텍스트 패널
    public Button button;
    public TMP_Text buttonText; // 버튼 문구

    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCol;
    SpriteRenderer spren;
    EnemyAudio enemyAudio; // 보스에게 붙어있는 EnemyAudio 스크립트

    Vector2 offset;         // 플레이어와 보스의 거리
    public LayerMask layer; // 플레이어의 레이어를 담고있는 변수
    public int bossHp;      
    public int bossSpeed;   
    public int bulletSpeed = 6;
    public float timer;    
    public float coTimer;   // 코루틴 타이머
    bool isDie = false; // 죽었는지 
    public bool isRandomFire = true; 
    public bool isAroundFire = true;
    public bool isFires = true;

    bool isStart = false;// 발악패턴이 시작되었는가

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
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        EnemyFilp();
        StateBossPatton();
    }
    private void FixedUpdate()
    {
        //StateBossPatton();
    }

    void Init() // 초기값 
    {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if (capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
            capCol.enabled = true;
        }
        if (spren == null) spren = GetComponent<SpriteRenderer>();
        if (enemyAudio == null) enemyAudio = GetComponent<EnemyAudio>();
    }

    void StateBossPatton() // 보스의 패턴 상태
    {
        if (startCo == true)
        {
            startCo = false;
            rigid.velocity = Vector2.zero;
            switch (curBossPatton)
            {
                case BossPatton.SpinFire: // 적의 위치를 받아 연속으로 발사 
                    StartCoroutine(SpinFire());

                    break;
                case BossPatton.Fires:
                    StartCoroutine(IFires());

                    //Fires();
                    // 플레이어 방향으로 샷건
                    break;
                case BossPatton.AroundFire:
                    StartCoroutine(IAroundFire());

                    //AroundFire();
                    // 원 형태로 전체 공격;
                    break;
                case BossPatton.Teleport:
                    StartCoroutine(ITeleport());

                    // 순간이동
                    //Teleport();
                    break;
                case BossPatton.ExploreFire:
                    StartCoroutine(IExploreFire());

                    //ExploreFire();
                    break;
                case BossPatton.ShotgunFire:
                    StartCoroutine(IShotgunFire());
                    break;
            }
        }
    }
    IEnumerator ITeleport() // 플레이어의 위치로 순간이동 패턴
    {
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isTLportOn", true);
        capCol.enabled = false;


        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isTLportOff", true);
        anim.SetBool("isTLportOn", false);
        capCol.enabled = true;

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isTLportOff", false);
        startCo = true;
        SetPatton();
    }
   
   

    IEnumerator IFires() // 적에게 빠르게 8발의 불렛을 발사하는 패턴
    {
        Vector2 dir;
        anim.SetBool("isMove", true);
        while(count < 8)
        {
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1,2));
            dir += offset;
            rigid.velocity = offset.normalized * bossSpeed;
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, 7);
            enemyAudio.PlayAudio((int)BossAudio.Fires);
            count += 1;
            yield return new WaitForSeconds(0.2f);
        }
        anim.SetBool("isMove", false);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        count = 0;
        startCo = true;
        SetPatton();
    }
   
    IEnumerator IAroundFire() // 전방향으로 한번에 불렛을 발사하는 패턴
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMove", false);
        anim.SetBool("isJump", true);   
    }

    IEnumerator IExploreFire() // 벽에 닿으면 폭발하여 여려개의 불렛을 발사하는 패턴
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isExploreFire", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isExploreFire", false);
        yield return new WaitForSeconds(0.5f);
        startCo = true;
        SetPatton();
    }
    
    IEnumerator IShotgunFire() // 3발, 4발의 불렛을 발사하는 패턴
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShotgunThree", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShotgunThree", false);
        anim.SetBool("isShotgunFive", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShotgunFive", false);
        startCo = true;
        SetPatton();
    }

    IEnumerator SpinFire() // 플레이어의방향으로 부채꼴로 불렛을 발사하는 패턴
    {
        Vector2 dir;
        BossBullet obj;
        int count = 0;
        int shotCount = 5;
        offsetDir = offset;
        while (count < shotCount)
        {
            for (int i = -7; i < shotCount; i++) //
            {
                dir = Quaternion.AngleAxis(10 * i, Vector3.forward) * offsetDir;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed);
                enemyAudio.PlayAudio((int)BossAudio.Fires);
                count++;
                yield return new WaitForSeconds(0.1f);
            }
        }
        startCo = true;
        SetPatton();
    }

    IEnumerator DieFire() // 보스의 체력이 0이되어 죽었을 때 발생하는 발악 패턴
    {
        isDie = true;
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", false);
        capCol.enabled = false;
        yield return new WaitForSeconds(2f);
        anim.SetBool("isDieFireTLportOn", true);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDieFireTLportOff", true);
        anim.SetBool("isDieFireTLportOn", false);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDieFireStart", true);
        anim.SetBool("isDieFireTLportOff", false);

        yield return new WaitForSeconds(0.5f);
        Vector2 dir;
        BossBullet obj;
        int shotCount = 500;
        int ddd = 0;  // 불렛이 나가는 방향을 다르게 하기위해 추가
        //offsetDir = offset;
        isStart = true;

        while (isStart == true)
        { 
            for (int i = -10; i < shotCount; i++) //
            {
                dir = Quaternion.AngleAxis((15 * i) + ddd, Vector3.forward) * transform.position;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed);
                ddd += 1;
                coTimer += 0.02f;
                yield return new WaitForSeconds(0.02f);
                if (coTimer >= 6f)
                {
                    coTimer = 0;
                    isStart = false;
                    break;
                }
            }
        }

        anim.SetBool("isDieFireStart", false);
        anim.SetTrigger("isDie");
        
        yield return new WaitForSeconds(1.5f);
        Ending();
    }
    

    void SetPatton() // 패턴을 랜덤으로 봐꿔주는 함수
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                curBossPatton = BossPatton.SpinFire;
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
            case 5:
                curBossPatton = BossPatton.ShotgunFire;
                break;
        }
    }
    void EnemyFilp() // 싱글톤에 있는 플레이어의 위치에 따라 좌우반전시켜주는 함수
    {
        Vector3 localScale = transform.localScale;
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1; 
        transform.localScale = localScale; 
    }

    public void IsHit(int damage =1) // 보스의 히트 함수
    {
        bossHp -= damage;
        
        StartCoroutine(IsHitColorChange());
        if(bossHp <= 0)
        {
            StopAllCoroutines();

            StartCoroutine(DieFire());
        }
    }
    void Ending() // 보스의 발악패턴이 끝난 후 나오는 승리 문구
    {
        GameManager.Instance.Player.IsStop = true; // 백인범이 추가함
        textObj.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = "Game Clear";
        buttonText.text = "RESTART";
        Time.timeScale = 0;
    }
    IEnumerator IsHitColorChange() // 보스가 히트될 때마다 색상변경
    {
        spren.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        spren.color = Color.white;
    }
    void AroundFireOn() // 애니메이션 점프모션이 땅바닥에 닿았을 때 실행되는 함수
    {
        BossBullet bullet;
        Vector2 dir;
        int count = 30; // 총알 갯수
        float intervalAngle = 360 / count; // 총알의 각도
        
        for (int i = 0; i < count; ++i)
        {
            bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            float angle = intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
            dir = new Vector2(x, y);
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, bulletSpeed);
            enemyAudio.PlayAudio((int)BossAudio.AroundFire);
        }
        anim.SetBool("isJump", false);
        startCo = true;
        SetPatton();
    }

    void TeleportOn() // 텔레포트 애니메이션에 넣을 함수
    {
        enemyAudio.PlayAudio((int)BossAudio.TLport);
        this.transform.position = EnemyObjectPool.instance.player.transform.position;
    }
    void ExploreFireOn() // 폭발불렛 애니메이션에 넣을 함수 
    {
        enemyAudio.PlayAudio((int)BossAudio.Explore);
        ExploreBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetExploreBullet();
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        Vector2 dir = new Vector2(x, y);
        dir += offset;
        bullet.transform.position = transform.position;
        bullet.SetRigidExploreBullet(dir, 7);
    }
    void ThreeFireOn() // 샷건 3발을 발사하는 애니메이션에 넣을 함수
    {
        Vector2 dir;
        BossBullet obj;
        for (int i = -1; i < 2; i++) //
        {
            dir = Quaternion.AngleAxis(10 * i, Vector3.forward) * offset;
            obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            obj.transform.position = transform.position;
            obj.SetRigidBossBullet(dir, bulletSpeed);
            enemyAudio.PlayAudio((int)BossAudio.ThreeFire);
        }
    }
    void FiveFireOn() // 샷건 5발을 발사하는 애니메이션에 넣을 함수
    {
        Vector2 dir;
        BossBullet obj;
        for (int i = -2; i < 3; i++) //
        {
            if (i == 0)
            {
                continue;
            }
            if (2 == Mathf.Abs(i))
            {
                dir = Quaternion.AngleAxis(9 * i, Vector3.forward) * offset;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed);
            }
            if (1 == Mathf.Abs(i))
            {
                dir = Quaternion.AngleAxis(6 * i, Vector3.forward) * offset;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed);
            }
            enemyAudio.PlayAudio((int)BossAudio.FiveFire);
        }
    }
    void DieTLport() // 보스가 죽었을 때 텔레포트애니메이션에 넣어줄 함수
    {
        transform.position = new Vector2(0, 5);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) // 보스의 콜리더에 닿았을 때 플레이어에게 대미지를 주는 함수
    {
        if (isDie == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Character>().PlayerHIt();
            }
        }
    }

}

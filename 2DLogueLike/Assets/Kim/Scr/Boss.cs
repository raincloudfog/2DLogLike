using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    //public CameraMove cameraShaker;
    public enum BossPatton
    {
        SpinFire,
        Fires,
        AroundFire,
        Teleport,
        ExploreFire,
        ShotgunFire
    }

    public enum BossAudio
    {
        Fires,
        Explore,
        TLport
    }

    [SerializeField] ExploreBullet exploreBullet;
    
    public BossPatton curBossPatton = BossPatton.Fires;
    public TMP_Text text;
    public GameObject textObj;
 
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCol;
    SpriteRenderer spren;
    EnemyAudio enemyAudio;

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
    void Move() // ������ �Լ� ���� ������ �׾��� ��� ���Ͻ��� �۵����� �ʰ� �Ѵ�
    {
        if (isDie == true)
        {
            return;
        }
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        //rigid.velocity = offset.normalized * bossSpeed;
        EnemyFilp();
        StateBossPatton();
    }

    void StateBossPatton() // ������ ���� ����
    {
        if (startCo == true)
        {
            startCo = false;
            rigid.velocity = Vector2.zero;
            switch (curBossPatton)
            {
                case BossPatton.SpinFire: // ���� ��ġ�� �޾� �������� �߻� 
                    StartCoroutine(SpinFire());

                    break;
                case BossPatton.Fires:
                    StartCoroutine(IFires());

                    //Fires();
                    // �÷��̾� �������� ����
                    break;
                case BossPatton.AroundFire:
                    StartCoroutine(IAroundFire());

                    //AroundFire();
                    // �� ���·� ��ü ����;
                    break;
                case BossPatton.Teleport:
                    StartCoroutine(ITeleport());

                    // �����̵�
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

    void Init() // �ʱⰪ 
    {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if(capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
            capCol.enabled = true;
        }
        if (spren == null) spren = GetComponent<SpriteRenderer>();
        //if (bossAudio == null) bossAudio = GetComponent<AudioSource>();
        if (enemyAudio == null) enemyAudio = GetComponent<EnemyAudio>();
    }

    IEnumerator IRandomFire() // ���°� ������ �� �ѹ��� ����ǰ� ����
    {
        anim.SetBool("isMove", true);
        bool isStart = true;
        while (isStart == true)
        {
            coTimer += 0.1f;
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
            randBullet += offset;
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(randBullet, bulletSpeed, bulletDamage);
            yield return new WaitForSeconds(0.1f);
        }

    }
   
    IEnumerator ITeleport()
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
   
   

    IEnumerator IFires() // ������ ������ �ҷ��� 8���� �߻�
    {
        Vector2 dir;
        anim.SetBool("isMove", true);
        while(count < 8)
        {
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 2));
            dir += offset;
            rigid.velocity = offset.normalized * bossSpeed;
            BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            bullet.transform.position = transform.position;
            bullet.SetRigidBossBullet(dir, 7, bulletDamage);
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
   
    IEnumerator IAroundFire()
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMove", false);
        anim.SetBool("isJump", true);   
    }

    IEnumerator IExploreFire()
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
    
    IEnumerator IShotgunFire()
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

    IEnumerator DieFire()
    {
        isDie = true;
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", false);

        yield return new WaitForSeconds(2f);
        anim.SetBool("isDieFireTLportOn", true);
        capCol.enabled = false;

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDieFireTLportOff", true);
        anim.SetBool("isDieFireTLportOn", false);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDieFireStart", true);
        anim.SetBool("isDieFireTLportOff", false);

        yield return new WaitForSeconds(0.5f);
        Vector2 dir;
        BossBullet obj;
        int shotCount = 100;
        int ddd = 0;
        Vector2 offsetDir = offset;
        bool isStart = true;
        
        while (isStart == true)
        {
            coTimer += 2f;
            if (coTimer >= 10f)
            {
                coTimer = 0;
                isStart = false;
            }
            for (int i = -10; i < shotCount; i++) //
            {
                /*if (i==0)
                {
                    continue;
                }*/
                dir = Quaternion.AngleAxis((15 * i) + ddd, Vector3.forward) * transform.position;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
                yield return new WaitForSeconds(0.02f);
                ddd += 1;
            }
        }
        anim.SetBool("isDieFireStart", false);
        anim.SetTrigger("isDie");
        
        yield return new WaitForSeconds(1.5f);
        Ending();

    }
    IEnumerator SpinFire()
    {
        
        Vector2 dir;
        BossBullet obj;
        int count = 0;
        int shotCount = 5;
        Vector2 offsetDir = offset;
        while (count < shotCount)
        {
            for (int i = -7; i < shotCount; i++) //
            {
                /*if (i==0)
                {
                    continue;
                }*/
                dir = Quaternion.AngleAxis(10 * i, Vector3.forward) * offsetDir;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
                yield return new WaitForSeconds(0.1f);
                count++;
            }
        }
        startCo = true;
        SetPatton();
    }

    void SetPatton() // ������ �������� �����ִ� �Լ�
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
    void EnemyFilp() // �̱��濡 �ִ� �÷��̾��� ��ġ�� ���� �¿���������ִ� �Լ�
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

    public void IsHit(int damage =1) // ������ ��Ʈ �Լ�
    {
        bossHp -= damage;
        
        StartCoroutine(IsHitColorChange());
        if(bossHp <= 0)
        {
            StopAllCoroutines();

            StartCoroutine(DieFire());
        }
    }
    void Ending()
    {
        textObj.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = "Game Clear";
        Time.timeScale = 0;
    }
    IEnumerator IsHitColorChange()
    {
        spren.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        spren.color = Color.white;
    }


    void AroundFireOn() // �ִϸ��̼� ��������� ���ٴڿ� ����� �� ����
    {
        int count = 30; // �Ѿ� ����
        float intervalAngle = 360 / count; // �Ѿ��� ����
        float weightAngle = 0; // ������ ���̸� �ֱ� ���� 
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
        startCo = true;
        SetPatton();
    }

    void TeleportOn() // �ڷ���Ʈ �ִϸ��̼ǿ� ���� �Լ�
    {
        
        enemyAudio.PlayAudio((int)BossAudio.TLport);
        this.transform.position = EnemyObjectPool.instance.player.transform.position;
    }
    void ExploreFireOn() // 
    {
        //PlaySound("ExploreSound");
        enemyAudio.PlayAudio((int)BossAudio.Explore);
        ExploreBullet bullet = Instantiate(exploreBullet);
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        Vector2 dir = new Vector2(x, y);
        dir += offset;
        bullet.transform.position = transform.position;
        bullet.SetRigidExploreBullet(dir, 7, bulletDamage);
    }
    void ThreeFireOn()
    {
        Vector2 dir;
        BossBullet obj;
        for (int i = -1; i < 2; i++) //
        {
            /*if (i==0)
            {
                continue;
            }*/
            dir = Quaternion.AngleAxis(10 * i, Vector3.forward) * offset;
            obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
            obj.transform.position = transform.position;
            obj.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
        }
    }
    void FiveFireOn()
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
                obj.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
            }
            if (1 == Mathf.Abs(i))
            {
                dir = Quaternion.AngleAxis(6 * i, Vector3.forward) * offset;
                obj = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                obj.transform.position = transform.position;
                obj.SetRigidBossBullet(dir, bulletSpeed, bulletDamage);
            }
        }
    }
    void DieTLport()
    {
        transform.position = new Vector2(0, 5);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDie == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Character>().PlayerHIt(1);
            }
        }
    }

}

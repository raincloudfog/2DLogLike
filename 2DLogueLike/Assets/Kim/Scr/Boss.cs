using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public enum BossPatton // ������ ��������
    {
        SpinFire,
        Fires,
        AroundFire,
        Teleport,
        ExploreFire,
        ShotgunFire
    }

    public enum BossAudio // ������ ���� ����
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
    public TMP_Text text;       //������ �׾��� �� ������ �¸�����
    public GameObject textObj; // �ؽ�Ʈ �г�
    public Button button;
    public TMP_Text buttonText; // ��ư ����

    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D capCol;
    SpriteRenderer spren;
    EnemyAudio enemyAudio; // �������� �پ��ִ� EnemyAudio ��ũ��Ʈ

    Vector2 offset;         // �÷��̾�� ������ �Ÿ�
    public LayerMask layer; // �÷��̾��� ���̾ ����ִ� ����
    public int bossHp;      
    public int bossSpeed;   
    public int bulletSpeed = 6;
    public float timer;    
    public float coTimer;   // �ڷ�ƾ Ÿ�̸�
    bool isDie = false; // �׾����� 
    public bool isRandomFire = true; 
    public bool isAroundFire = true;
    public bool isFires = true;

    bool isStart = false;// �߾������� ���۵Ǿ��°�

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

    void Init() // �ʱⰪ 
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
    IEnumerator ITeleport() // �÷��̾��� ��ġ�� �����̵� ����
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
   
   

    IEnumerator IFires() // ������ ������ 8���� �ҷ��� �߻��ϴ� ����
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
   
    IEnumerator IAroundFire() // ���������� �ѹ��� �ҷ��� �߻��ϴ� ����
    {
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isMove", false);
        anim.SetBool("isJump", true);   
    }

    IEnumerator IExploreFire() // ���� ������ �����Ͽ� �������� �ҷ��� �߻��ϴ� ����
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
    
    IEnumerator IShotgunFire() // 3��, 4���� �ҷ��� �߻��ϴ� ����
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

    IEnumerator SpinFire() // �÷��̾��ǹ������� ��ä�÷� �ҷ��� �߻��ϴ� ����
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

    IEnumerator DieFire() // ������ ü���� 0�̵Ǿ� �׾��� �� �߻��ϴ� �߾� ����
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
        int ddd = 0;  // �ҷ��� ������ ������ �ٸ��� �ϱ����� �߰�
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
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1; 
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
    void Ending() // ������ �߾������� ���� �� ������ �¸� ����
    {
        GameManager.Instance.Player.IsStop = true; // ���ι��� �߰���
        textObj.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = "Game Clear";
        buttonText.text = "RESTART";
        Time.timeScale = 0;
    }
    IEnumerator IsHitColorChange() // ������ ��Ʈ�� ������ ���󺯰�
    {
        spren.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        spren.color = Color.white;
    }
    void AroundFireOn() // �ִϸ��̼� ��������� ���ٴڿ� ����� �� ����Ǵ� �Լ�
    {
        BossBullet bullet;
        Vector2 dir;
        int count = 30; // �Ѿ� ����
        float intervalAngle = 360 / count; // �Ѿ��� ����
        
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

    void TeleportOn() // �ڷ���Ʈ �ִϸ��̼ǿ� ���� �Լ�
    {
        enemyAudio.PlayAudio((int)BossAudio.TLport);
        this.transform.position = EnemyObjectPool.instance.player.transform.position;
    }
    void ExploreFireOn() // ���ߺҷ� �ִϸ��̼ǿ� ���� �Լ� 
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
    void ThreeFireOn() // ���� 3���� �߻��ϴ� �ִϸ��̼ǿ� ���� �Լ�
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
    void FiveFireOn() // ���� 5���� �߻��ϴ� �ִϸ��̼ǿ� ���� �Լ�
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
    void DieTLport() // ������ �׾��� �� �ڷ���Ʈ�ִϸ��̼ǿ� �־��� �Լ�
    {
        transform.position = new Vector2(0, 5);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) // ������ �ݸ����� ����� �� �÷��̾�� ������� �ִ� �Լ�
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rigid; // �÷��̾��� ������κ��� �迵���� ������


    
    private AudioSource audioSource;  // �ѼҸ��� ����� ����� �ҽ�

    public Transform gun;   // ���� ��ġ
    public GameObject bulletPrefab; // ���̵�����

    [SerializeField] Animator anim; // �ִϸ�����
    [SerializeField] SpriteRenderer spr; // ��������Ʈ

    private Vector3 mousePosition;

    public float bulletSpeed; // �Ѿ� �ӵ�

    public float spreadAngle = 10f; // �Ѿ� ���� ����

    Vector3 Player; // �÷��̾��� ������ġ
    public Npc SHOP;// ���� ���� ��ġ
    public Npc POTION; // ���� ��ġ

    Vector3 Weapon;
    Vector3 Potion;

    public float gunDistance = 1.0f;// ���콺 ��ġ�� �� ������ �Ÿ�
    [SerializeField]
    float moveSpeed; // �̵��ӵ�
    float timer = 0; // Ÿ�̸�
    public float timerdelay = 0; // �Ѿ� ������
    public int Damage = 5; // �⺻ ������
    public int Hp = 5; // ���� ü��
    public int MaxHp = 10; // �ִ� ü��
    public int Hpcut;
    public bool ismove = true; // �̵� ���
    public bool isAttack = true; // ���� ���
    [SerializeField] bool isHit = true; // Ÿ�� ���;
    float HealerDistance; // ���� ���ΰ��� �Ÿ�
    float shopDistance; // ������ΰ��� �Ÿ�

    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Hpcut = 50;
        
        
       if(SHOP != null && POTION != null)
       {
            Weapon = SHOP.transform.position;
            Potion = POTION.transform.position;
       }
        audioSource = GetComponent<AudioSource>();

        if (GameObject.Find("AudioManager"))
        {
            audioSource.clip = AudioManager.Instance.BulletSound[0];
        }




    }


    void Update()
    {
        gunDirection();
        Shoot();
        Move();
        gunmove(); 
        Cheat(); // ���� ���

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (SHOP != null || POTION != null)
            {


                //NPC�� ����� ���� üũ..
                if (shopDistance < 5)
                {
                    SHOP.EventUI(true);

                }
                else if (HealerDistance < 5)
                {
                    POTION.EventUI(true);
                }
            }
            
        }
        if (SHOP != null || POTION != null)
        {
            if (shopDistance > 5 && HealerDistance > 5)
            {
                SHOP.EventUI(false);
                POTION.EventUI(false);
            }
        }
        
        

        //
    }
    private void FixedUpdate()
    {
        Player = transform.position;
        shopDistance = (Player - Weapon).sqrMagnitude;
        HealerDistance = (Player - Potion).sqrMagnitude;

        // ���� �������� �����ÿ� �ø����� �����ش�.
        // ���� ���������� ������ �ø��� �����Ѵ�.
        if (gun.rotation.eulerAngles.z >= 90 && gun.rotation.eulerAngles.z <= 270)
        {
            gun.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            spr.flipX = true;

        }
        else
        {
            gun.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            spr.flipX = false;

        }
        //Debug.Log(gun.rotation.eulerAngles.z); // �� ���� Ȯ�ΰ�
        AnimatorMove();
        
        
    }

    void Cheat() // ġƮ ���
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isHit = !isHit;
        }
    }

    private void AnimatorMove() // ���� ������
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        anim.SetFloat("State", (transform.position.x+worldPosition.x));
        /*if(anim.GetFloat("State") < 0)
        {
            spr.flipX = true;
        }
        else if (anim.GetFloat("State") > 0)
        {
            spr.flipX = false;
        }*/ // ���̵�����
    }

    

    public void PlayerHIt(int Damage = 1) // �÷��̾� �ǰݽ� 
    {
        if (isHit == false)
            return;
        Debug.Log("�¾���");
        Hp -= Damage;
        
        isHit = false;
        StartCoroutine(Hitspr());
        if(Hp <= 0)
        {
            Hp = 0;
            StartCoroutine(Die());
        }
    }

    IEnumerator Hitspr() //�÷��̾� ��Ʈ�� �ǰ� ���
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isHit = true;
        yield break;
    }

    void gunDirection() // �� ���� �ٿ��ְ��ϴ� �ڵ�
    {

        if(Time.timeScale == 1) // ���� �����߿��� �����̰� �ϴ� ���ǹ�
        {
            // ���콺 ��ġ�� ĳ������ ��ǥ��� ��ȯ
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // ���콺 ��ġ�� �� ������ ���͸� ���ϰ�, ���� �Ÿ���ŭ �ø���
            Vector3 gunOffset = (worldPosition - transform.position).normalized * gunDistance;

            // �� ��ġ ������Ʈ
            gun.position = transform.position + gunOffset;
        }        
    }

    void Shoot() // ���ǽ�� �Լ�
    {
        
        
        //Debug.Log("�ѽ�");
        if (GameManager.Instance.isUiactive == true)
        {
            return;
        }
        timer += Time.deltaTime;


        if (isAttack == false) //��ȭ���̰ų� �Ͻ������Ҷ��� ���߰�
        {
            return;
        }
        
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 world = Vector3.zero;
        world.x = worldPosition.x;
        world.y = worldPosition.y;

        // ���콺 ���� ��ư�� ������ �߻�
        if (Input.GetMouseButton(0) && timer >= timerdelay)
        {
            audioSource.Play();
            timer = 0;
            if (GunManager.Instance.waeponType == WaeponType.Shotgun)
            {

                Shotgun();
                
            }
            else
            {
                // �߻� ���� ���
                Vector2 direction = (world - transform.position).normalized;

                Debug.Log(direction);

                // �Ѿ� ����
                GameObject bullet = ObjectPoolBaek.Instance.PlayerBulletCreate();
                bullet.transform.position = transform.GetChild(0).position;

                // �Ѿ˿� ���� ���� �߻�
                //bullet.GetComponent<Rigidbody2D>().velocity = direction * 100f ;
                bullet.GetComponent<Playerbullet>().Shoot(direction, bulletSpeed);
            }
           
            
        }
    }

    

    void Move() // �̵� �Լ�
    {
        if (ismove == false)
        {
            return;
        }
        // �÷��̾��� ������ x �� y����� Ȯ��
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // �÷��̾��� ������ Ȯ��
        Vector3 movedic = new Vector3();
        movedic.x = x;
        movedic.y = y;

        // �÷��̾ �����̴°� ���� �ӵ� ����
        //transform.position += movedic * Time.deltaTime * moveSpeed;

        // �迵���� ������ٵ�� ������ ����
        rigid.velocity = movedic * moveSpeed;
        anim.SetBool("isMove", movedic != Vector3.zero); // �迵���� �߰��� Ű�Է��� ���� �� �ִϸ��̼� ����   
    }

    void gunmove() // �� �̵� �Լ�
    {
        if(Time.timeScale == 0)// �Ͻ������϶� 
        {
            return;
        }
        if(isAttack == false ) // ��� �����ϸ�˵ɶ�
        {
            return;
        }
        // ���콺 ��ġ�� ĳ������ ��ǥ��� ��ȯ
        mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // �� ���� ���
        Vector3 direction = (mousePosition - transform.position).normalized;

        // �� ���� ��ȯ
        gun.right = direction;
        
    }

    public void Shotgun() // ������ �����߷� �����ϴ�.
    {
        

        for (int i = -1; i <2 ; i++)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 world = Vector3.zero;
            world.x = worldPosition.x;
            world.y = worldPosition.y;

            // ���콺 Ŀ���� ��ġ�� ���� ��ǥ��� ��ȯ
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // �Ѿ� �߻� ���� ����
            Vector2 bulletDirection = (world - transform.position).normalized;

            // �Ѿ� �߻� ���� ���͸� �����ϰ� ����
            float spread = Random.Range(-spreadAngle, spreadAngle);
            bulletDirection = Quaternion.AngleAxis(10 * i, Vector3.forward) * bulletDirection;
            GameObject bullet = ObjectPoolBaek.Instance.PlayerBulletCreate();
            bullet.transform.position = transform.GetChild(0).transform.position;
            bullet.GetComponent<Playerbullet>().Shoot(bulletDirection, bulletSpeed);
        }
        // �Ѿ� �߻�
        
        
    }

    IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        OptionManager.Instance.died.SetActive(true);
        Time.timeScale = 0;
    }

}

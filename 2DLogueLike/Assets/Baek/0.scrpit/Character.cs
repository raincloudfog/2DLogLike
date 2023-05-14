using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{


    public Transform gun;   // ���� ��ġ
    public GameObject bulletPrefab;

    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spr;

    private Vector3 mousePosition;

    public float bulletSpeed; // �Ѿ� �ӵ�

    public float spreadAngle = 10f; // �Ѿ� ���� ����



    public float gunDistance = 1.0f;// ���콺 ��ġ�� �� ������ �Ÿ�
    [SerializeField]
    float moveSpeed;
    float timer = 0;
    public float timerdelay = 0;
    public int Damage = 5;
    public int Hp = 10;
    public int MaxHp = 100;
    public int Hpcut;
    public bool ismove = true; // �̵� ���
    public bool isAttack = true; // ���� ���
    [SerializeField] bool isHit = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        Hpcut = 50;
    }


    void Update()
    {
        gunDirection();
        Shoot();
        Move();
        gunmove();
        
    }

    private void AnimatorMove()
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

    private void FixedUpdate()
    {
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

    public void PlayerHIt(int Damage)
    {
        if (isHit == false)
            return;
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
        transform.position += movedic * Time.deltaTime * moveSpeed;
        
        
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
        

        for (int i = 0; i < 5; i++)
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
            bulletDirection = Quaternion.Euler(spread, spread, 0f) * bulletDirection;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rigid; // 플레이어의 리지드부분은 김영수가 수정중


    
    private AudioSource audioSource;  // 총소리를 재생할 오디오 소스

    public Transform gun;   // 총의 위치
    public GameObject bulletPrefab; // 더미데이터

    [SerializeField] Animator anim; // 애니메이터
    [SerializeField] SpriteRenderer spr; // 스프라이트

    private Vector3 mousePosition;

    public float bulletSpeed; // 총알 속도

    public float spreadAngle = 10f; // 총알 퍼짐 각도

    Vector3 Player; // 플레이어의 현재위치
    public Npc SHOP;// 무기 상인
    public Npc POTION; // 힐러 

    Vector3 Weapon; // 무기상인의 위치
    Vector3 Potion; // 힐러의 위치

    public float gunDistance = 1.0f;// 마우스 위치와 총 사이의 거리
    [SerializeField]
    float moveSpeed; // 이동속도
    float timer = 0; // 타이머
    public float timerdelay = 0; // 총알 딜레이
    public int Damage = 5; // 기본 데미지
    public int Hp = 5; // 현재 체력
    public int MaxHp = 10; // 최대 체력
    public int Hpcut;
    public bool ismove = true; // 이동 허용
    public bool isAttack = true; // 공격 허용
    [SerializeField] bool isHit = true; // 타격 허용;
    float HealerDistance; // 힐러 상인과의 거리
    float shopDistance; // 무기상인과의 거리

    public bool isEnemy = false;
    private bool isStop = false;
    public bool IsStop
    {
        get { return isStop; }
        set
        {
            isStop = value;
        }
    }

    // 김영수 담당
    bool isinvincibility = false; // 무적 -> 기본은 false
    bool isDie = false;
    bool isDash = false;
    bool isDashTime = false;

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
        Cheat(); // 무적 모드

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (SHOP != null || POTION != null)
            {


                //NPC랑 가까운 뭔지 체크..
                if (shopDistance < 5)
                {
                    
                    if (SHOP.GetComponent<EnemyNPC>().enabled == false)
                        SHOP.EventUI(true);

                }
                else if (HealerDistance < 5)
                {
                    if(isEnemy.Equals(true))
                    {
                        return;
                    }
                    if (SHOP.GetComponent<EnemyNPC>().enabled == false)
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

        // 총이 왼쪽으로 갔을시에 플립으로 돌려준다.
        // 총이 오른쪽으로 갔을시 플립을 해제한다.
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
        //Debug.Log(gun.rotation.eulerAngles.z); // 총 각도 확인값
        AnimatorMove();
        
        
    }

    void Cheat() // 치트 모드
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isinvincibility = !isinvincibility;
        }
    }

    private void AnimatorMove() // 더미 데이터
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
        }*/ // 더미데이터
    }

    

    public void PlayerHIt(int Damage = 1) // 플레이어 피격시 
    {
        if(isinvincibility == true) // 무적 코드
        {
            return;
        }
        if (isHit == false) // 피격 딜레이
            return;
        //Debug.Log("맞았음");
        Hp -= Damage;
        //Debug.Log(Hp + "/" + Damage);
        isHit = false;
        StartCoroutine(Hitspr());
        if(Hp <= 0) // 죽었을시 피 더안깍이도록
        {
            Hp = 0;
            StartCoroutine(Die());
        }
    }

    IEnumerator Hitspr() //플레이어 히트시 피격 모션
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isHit = true;
        yield break;
    }

    void gunDirection() // 총 옆에 붙여있게하는 코드
    {

        if(Time.timeScale == 1) // 게임 진행중에만 움직이게 하는 조건문
        {
            // 마우스 위치를 캐릭터의 좌표계로 변환
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // 마우스 위치와 총 사이의 벡터를 구하고, 일정 거리만큼 늘린다
            Vector3 gunOffset = (worldPosition - transform.position).normalized * gunDistance;

            // 총 위치 업데이트
            gun.position = transform.position + gunOffset;
        }        
    }

    void Shoot() // 샷건쏘는 함수
    {
        
        
        //Debug.Log("총쏨");
        if (GameManager.Instance.isUiactive == true)
        {
            return;
        }
        timer += Time.deltaTime;


        if (isAttack == false) //대화중이거나 일시정지할때는 멈추게
        {
            return;
        }
        
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 world = Vector3.zero;
        world.x = worldPosition.x;
        world.y = worldPosition.y;

        // 마우스 왼쪽 버튼을 누르면 발사
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
                // 발사 방향 계산
                Vector2 direction = (world - transform.position).normalized;

                //Debug.Log(direction);

                // 총알 생성
                GameObject bullet = ObjectPoolBaek.Instance.PlayerBulletCreate();
                bullet.transform.position = transform.GetChild(0).position;

                // 총알에 힘을 가해 발사
                //bullet.GetComponent<Rigidbody2D>().velocity = direction * 100f ;
                bullet.GetComponent<Playerbullet>().Shoot(direction, bulletSpeed);
            }
           
            
        }
    }

    

    void Move() // 이동 함수
    {
        if(isStop == true)
        {
            return;
        }

        if (ismove == false)
        {
            return;
        }
        // 플레이어의 움직임 x 축 y축방향 확인
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // 플레이어의 움직임 확인
        Vector3 movedic = new Vector3();
        movedic.x = x;
        movedic.y = y;

        // 플레이어가 움직이는걸 실행 속도 조절
        //transform.position += movedic * Time.deltaTime * moveSpeed;

        // 김영수가 리지드바디로 움직임 수정 
        // 키입력시 대쉬가 발동하며 잠깐의 무적 
        if(Input.GetKeyDown(KeyCode.LeftShift)&& isDash == false && rigid.velocity != Vector2.zero)
        {
            StartCoroutine(Dash(movedic));
        }
        if (isDashTime == false)
            // 대쉬중이 아닐 때 움직임 -> 대쉬중에는 방향이 입력되면 안되기 때문
        {
            rigid.velocity = movedic.normalized * moveSpeed;
        }
        
        anim.SetBool("isMove", movedic != Vector3.zero); // 김영수가 추가함 키입력이 있을 때 애니메이션 실행   
    }

    IEnumerator Dash(Vector3 dir) //김영수 담당
    {
        isDash = true;
        isDashTime = true;
        isHit = false;
        rigid.velocity = dir.normalized * moveSpeed * 2;
        spr.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        spr.color = Color.white;
        isDashTime = false;
        isHit = true;
        yield return new WaitForSeconds(1f);
        isDash = false;
    }

    void gunmove() // 총 이동 함수
    {
        if(Time.timeScale == 0)// 일시정지일때 
        {
            return;
        }
        if(isAttack == false ) // 잠시 공격하면알될때
        {
            return;
        }
        // 마우스 위치를 캐릭터의 좌표계로 변환
        mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 총 방향 계산
        Vector3 direction = (mousePosition - transform.position).normalized;

        // 총 방향 전환
        gun.right = direction;
        
    }

    public void Shotgun() // 샷건은 여러발로 나갑니다.
    {
        

        for (int i = -1; i <2 ; i++)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 world = Vector3.zero;
            world.x = worldPosition.x;
            world.y = worldPosition.y;

            // 마우스 커서의 위치를 월드 좌표계로 변환
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // 총알 발사 방향 벡터
            Vector2 bulletDirection = (world - transform.position).normalized;

            // 총알 발사 방향 벡터를 랜덤하게 변경
            float spread = Random.Range(-spreadAngle, spreadAngle);
            bulletDirection = Quaternion.AngleAxis(10 * i, Vector3.forward) * bulletDirection;
            GameObject bullet = ObjectPoolBaek.Instance.PlayerBulletCreate();
            bullet.transform.position = transform.GetChild(0).transform.position;
            bullet.GetComponent<Playerbullet>().Shoot(bulletDirection, bulletSpeed);
        }
        // 총알 발사
        
        
    }

    IEnumerator Die() // 죽었을때
    {
        anim.SetTrigger("Die"); // 죽는 애니메이션 발동
        ismove = false; // 못움직이게
        isAttack = false; // 총 못쏘게
        rigid.velocity = Vector2.zero; // 이동 속도 거기서 멈춤
        yield return new WaitForSeconds(1f); // 1초후 게임 오버 UI켜주기
        OptionManager.Instance.died.SetActive(true);
        Time.timeScale = 0; // 시간 안흐르게 막아주기
    }

}

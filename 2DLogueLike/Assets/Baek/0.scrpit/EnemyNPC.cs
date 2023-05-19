using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    public Transform player; // 플레이어의 위치
    public GameObject bulletPrefab; // NPC 총알 프리팹
    public Transform bulletSpawnPoint; // 총알 소환 위치
    public float fireInterval = 1f; 

    [SerializeField] GameObject Gun; // 총을 장착시켜주려했으나 에너지파를 쏘는것같은 총알이라 넣어주지 않았습니다.
    [NonReorderable]
    [SerializeField] int HP = 20;
    [SerializeField] float speed = 2f;
    [SerializeField] bool isHit = true;
    private float timer = 0f;
    [SerializeField] SpriteRenderer spr;
    
    [SerializeField] GameObject roomdoor; // NPC를 죽였을경우 히든방 문이 열럽니다.
 

    void Init() // 스프라이트 확인후 없으면 넣어주기
    {
        if(spr == null)
        {
            spr = GetComponent<SpriteRenderer>();
            
        }
        
    }
    
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval) // 공격 딜레이
        {
            Fire();
            timer = 0f;
        }
        if(HP <= 0) // 죽었을시
        {
            
            roomdoor.SetActive(false);
            gameObject.SetActive(false);
        }
    }


    public void IsHit(int Damage) // 피격시 
    {
        Init();
        if (isHit == true) // 피격시 무적시간
        {
            HP -= Damage;
            isHit = false;
            StartCoroutine(Hitmotion());
        }

    }

    IEnumerator Hitmotion() // 피격시 색깔 변경
    {
        spr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        isHit = true;
        spr.color = new Color(1, 1, 1, 1);
        yield break;
    }


    private void Fire() // 공격하기
    {
        if (player == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = player;
    }



}



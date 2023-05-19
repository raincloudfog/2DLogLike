using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D hit; // 총알이 무언가에 부딪힐때 감지
    //속도 변수 
    Vector2 speed = Vector2.zero;
    //발사
    //재발사
    //정지
    [SerializeField] Sprite[] bullets; // 총알 종류 이나 구현못함.

    Character player; // 플레이어.


    private void OnEnable()
    {
        if(player == null)
        {
            player = ObjectPoolBaek.Instance.Player;
        }
        GetComponent<SpriteRenderer>().sprite = bullets[0]; // 총알 여러개의 스프라이트로 하려했으나 구현 못함.
    }

    public void Shoot(Vector2 dir, float bulletSpeed) // 총알의 속도를 정해줍니다.
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        speed = dir* bulletSpeed; // 받은 벡터값
        rigid.velocity = speed;
    }
    public void ShootAgain() // 만약 일시정지 시켰을우 속도가 제로이니까 저장해놓은 스피드를 불러옵니다.
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = speed; // 저장해놓은 스피드 불러오기
    }
    public void Stop() // 일시정지 시켰을때 총알 속도들을 전부 멈춤
    {
        rigid.velocity = Vector2.zero; // 총알의 속도를 멈추기.
    }

    private void FixedUpdate()
    {
        hit = Physics2D.OverlapCircle(transform.position,
            0.5f // 총알의 크기
            , LayerMask.GetMask("Wall", "Enemy", "NPC")); // 총알이 만약 벽이나 적에게 닿였을경우
        if (hit != null)// 히트했을시 상황 발생
        {
            if (hit.gameObject.layer == 9) // 적일경우 데미지를 입는다.
            {
                if (hit.CompareTag("Boss")) // 보스일 경우
                {
                    hit.GetComponent<Boss>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                }
                
                else // 에너미일경우
                {
                    hit.GetComponent<Enemy>().IsHit(ObjectPoolBaek.Instance.Player.Damage);                   
                }

                Debug.Log(ObjectPoolBaek.Instance.Player.Damage);
                
            }

            if (hit.CompareTag("Shop")) // 만약 무기상인을 때렸을경우 적으로 변화시킴
            {
                player.isEnemy = true;
                hit.GetComponent<EnemyNPC>().enabled = true;
                hit.GetComponent<Npc>().enabled = false;
                hit.GetComponent<EnemyNPC>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                //hit.gameObject.layer = 9;
            }
            else if(hit.CompareTag("Healer")) // 만약 힐러를 때렸을경우 힐러는 사라집니다. 
            {
                player.isEnemy = true;
                hit.gameObject.SetActive(false);
            }
            if (GunManager.Instance.waeponType == WaeponType.Missile) // 만약 무기 탄환이 미사일일경우 총알이 부딪히면서 폭탄 이펙트를 생성합니다.
            {                
                GameObject boom = ObjectPoolBaek.Instance.BoomCreate();
                boom.transform.SetParent(null);// 생성된 붐 이펙트가 총알이랑 사라지면 안되므로 부모를 null로 바꿔줌.
                boom.transform.position = transform.position; // 폭탄이 생성되는 위치는 총알이 맞은 위치에서 

            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);            // 총알을 오브젝트풀에 다시 반환시킵니다.
        }

    }
}

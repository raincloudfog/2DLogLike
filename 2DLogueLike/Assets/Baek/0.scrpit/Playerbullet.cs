using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D hit;
    //속도 변수 
    Vector2 speed = Vector2.zero;
    //발사
    //재발사
    //정지
    [SerializeField] Sprite[] bullets;
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = bullets[0];
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
    public void Stop()
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
                if (hit.CompareTag("Boss"))
                {
                    hit.GetComponent<Boss>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                }
                else
                {
                    hit.GetComponent<Enemy>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                }

                Debug.Log(ObjectPoolBaek.Instance.Player.Damage);
                
            }

            if (hit.CompareTag("Shop"))
            {
                hit.GetComponent<Enemy>().enabled = true;
                hit.GetComponent<Npc>().enabled = false;
                hit.gameObject.layer = 9;
            }
            if (GunManager.Instance.waeponType == WaeponType.Missile)
            {
                Debug.Log("미사일 발사");
                GameObject boom = ObjectPoolBaek.Instance.BoomCreate();
                boom.transform.SetParent(null);
                boom.transform.position = transform.position;

            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);
        }

    }
    /*private void OnCollisionEnter2D(Collision2D collision) // collsion일때
    {
        
            if (collision.gameObject.layer == 9) // 적일경우 데미지를 입는다.
            {

                collision.gameObject.GetComponent<Enemy>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                Debug.Log(ObjectPoolBaek.Instance.Player.Damage);
            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);
        
    }*/
}

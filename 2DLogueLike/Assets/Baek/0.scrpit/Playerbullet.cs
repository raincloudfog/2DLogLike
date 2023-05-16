using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D hit;
    //�ӵ� ���� 
    Vector2 speed = Vector2.zero;
    //�߻�
    //��߻�
    //����
    [SerializeField] Sprite[] bullets;
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = bullets[0];
    }

    public void Shoot(Vector2 dir, float bulletSpeed) // �Ѿ��� �ӵ��� �����ݴϴ�.
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        speed = dir* bulletSpeed; // ���� ���Ͱ�
        rigid.velocity = speed;
    }
    public void ShootAgain() // ���� �Ͻ����� �������� �ӵ��� �����̴ϱ� �����س��� ���ǵ带 �ҷ��ɴϴ�.
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = speed; // �����س��� ���ǵ� �ҷ�����
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero; // �Ѿ��� �ӵ��� ���߱�.
    }

    private void FixedUpdate()
    {
        hit = Physics2D.OverlapCircle(transform.position,
            0.5f // �Ѿ��� ũ��
            , LayerMask.GetMask("Wall", "Enemy", "NPC")); // �Ѿ��� ���� ���̳� ������ �꿴�����
        if (hit != null)// ��Ʈ������ ��Ȳ �߻�
        {
            if (hit.gameObject.layer == 9) // ���ϰ�� �������� �Դ´�.
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
                Debug.Log("�̻��� �߻�");
                GameObject boom = ObjectPoolBaek.Instance.BoomCreate();
                boom.transform.SetParent(null);
                boom.transform.position = transform.position;

            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);
        }

    }
    /*private void OnCollisionEnter2D(Collision2D collision) // collsion�϶�
    {
        
            if (collision.gameObject.layer == 9) // ���ϰ�� �������� �Դ´�.
            {

                collision.gameObject.GetComponent<Enemy>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                Debug.Log(ObjectPoolBaek.Instance.Player.Damage);
            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);
        
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D hit; // �Ѿ��� ���𰡿� �ε����� ����
    //�ӵ� ���� 
    Vector2 speed = Vector2.zero;
    //�߻�
    //��߻�
    //����
    [SerializeField] Sprite[] bullets; // �Ѿ� ���� �̳� ��������.

    Character player; // �÷��̾�.


    private void OnEnable()
    {
        if(player == null)
        {
            player = ObjectPoolBaek.Instance.Player;
        }
        GetComponent<SpriteRenderer>().sprite = bullets[0]; // �Ѿ� �������� ��������Ʈ�� �Ϸ������� ���� ����.
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
    public void Stop() // �Ͻ����� �������� �Ѿ� �ӵ����� ���� ����
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
                if (hit.CompareTag("Boss")) // ������ ���
                {
                    hit.GetComponent<Boss>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                }
                
                else // ���ʹ��ϰ��
                {
                    hit.GetComponent<Enemy>().IsHit(ObjectPoolBaek.Instance.Player.Damage);                   
                }

                Debug.Log(ObjectPoolBaek.Instance.Player.Damage);
                
            }

            if (hit.CompareTag("Shop")) // ���� ��������� ��������� ������ ��ȭ��Ŵ
            {
                player.isEnemy = true;
                hit.GetComponent<EnemyNPC>().enabled = true;
                hit.GetComponent<Npc>().enabled = false;
                hit.GetComponent<EnemyNPC>().IsHit(ObjectPoolBaek.Instance.Player.Damage);
                //hit.gameObject.layer = 9;
            }
            else if(hit.CompareTag("Healer")) // ���� ������ ��������� ������ ������ϴ�. 
            {
                player.isEnemy = true;
                hit.gameObject.SetActive(false);
            }
            if (GunManager.Instance.waeponType == WaeponType.Missile) // ���� ���� źȯ�� �̻����ϰ�� �Ѿ��� �ε����鼭 ��ź ����Ʈ�� �����մϴ�.
            {                
                GameObject boom = ObjectPoolBaek.Instance.BoomCreate();
                boom.transform.SetParent(null);// ������ �� ����Ʈ�� �Ѿ��̶� ������� �ȵǹǷ� �θ� null�� �ٲ���.
                boom.transform.position = transform.position; // ��ź�� �����Ǵ� ��ġ�� �Ѿ��� ���� ��ġ���� 

            }
            ObjectPoolBaek.Instance.PlayerBulletReturn(gameObject);            // �Ѿ��� ������ƮǮ�� �ٽ� ��ȯ��ŵ�ϴ�.
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ
    public GameObject bulletPrefab; // NPC �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ� ��ȯ ��ġ
    public float fireInterval = 1f; 

    [SerializeField] GameObject Gun; // ���� ���������ַ������� �������ĸ� ��°Ͱ��� �Ѿ��̶� �־����� �ʾҽ��ϴ�.
    [NonReorderable]
    [SerializeField] int HP = 20;
    [SerializeField] float speed = 2f;
    [SerializeField] bool isHit = true;
    private float timer = 0f;
    [SerializeField] SpriteRenderer spr;
    
    [SerializeField] GameObject roomdoor; // NPC�� �׿������ ����� ���� �����ϴ�.
 

    void Init() // ��������Ʈ Ȯ���� ������ �־��ֱ�
    {
        if(spr == null)
        {
            spr = GetComponent<SpriteRenderer>();
            
        }
        
    }
    
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval) // ���� ������
        {
            Fire();
            timer = 0f;
        }
        if(HP <= 0) // �׾�����
        {
            
            roomdoor.SetActive(false);
            gameObject.SetActive(false);
        }
    }


    public void IsHit(int Damage) // �ǰݽ� 
    {
        Init();
        if (isHit == true) // �ǰݽ� �����ð�
        {
            HP -= Damage;
            isHit = false;
            StartCoroutine(Hitmotion());
        }

    }

    IEnumerator Hitmotion() // �ǰݽ� ���� ����
    {
        spr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        isHit = true;
        spr.color = new Color(1, 1, 1, 1);
        yield break;
    }


    private void Fire() // �����ϱ�
    {
        if (player == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.target = player;
    }



}



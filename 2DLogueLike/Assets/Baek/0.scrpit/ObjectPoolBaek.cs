using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolBaek : SingletonBaek<ObjectPoolBaek>
{
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }


    }

    [SerializeField] GameObject Boom; // 폭탄이펙트 오브젝트

    [SerializeField] GameObject hpiconlist; // HP가 담길 공간
    [SerializeField] Image Hpicon; // HP
    public Character Player; // 플레이어 캐릭터
    [SerializeField]
    GameObject Playerbullet; // 플레이어의 총알

    Queue<GameObject> PlayerBulletPools = new Queue<GameObject>(); // 플레이어의 총알 오브젝트풀링
    Queue<Image> PlayerHpicon = new Queue<Image>(); // HP아이콘 오브젝트 풀링
    Queue<GameObject> BoomPools = new Queue<GameObject>(); // 폭탄 이펙트가 담길 큐

    public void PlayerBulletReturn(GameObject obj) // 총알 리턴
    {
        PlayerBulletPools.Enqueue(obj);
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.SetActive(false);
    }

    public GameObject PlayerBulletCreate() // 총알 생성
    {
        if(PlayerBulletPools.Count <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newobj = Instantiate(Playerbullet);
                PlayerBulletPools.Enqueue(newobj);
                newobj.SetActive(false);
            }
        }

        GameObject obj = PlayerBulletPools.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void HpiconReturn(Image img) //HP아이콘반환받음
    {
        PlayerHpicon.Enqueue(img);
        img.transform.SetParent(null);
        img.gameObject.SetActive(false);
    }

    public void HpiconCreate() // 총알 생성
    {
        if (PlayerHpicon.Count <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Image newobj = Instantiate(Hpicon);
                PlayerHpicon.Enqueue(newobj);
                newobj.gameObject.SetActive(false);
            }
        }

       
        Image obj = PlayerHpicon.Dequeue();
        obj.gameObject.SetActive(true);
        obj.transform.SetParent(hpiconlist.transform);
        
       
    }

    public GameObject BoomCreate() // 폭탄 이펙트를 줍니다.
    {
        if(BoomPools.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newobj = Instantiate(Boom);
                BoomPools.Enqueue(newobj);
                newobj.SetActive(false);
            }
        }

        GameObject obj = BoomPools.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void BoomReturn(GameObject obj) // 폭탄 이펙트를 반환받습니다.
    {
        BoomPools.Enqueue(obj);
        
        obj.SetActive(false);
        
        obj.transform.SetParent(null);
    }
}

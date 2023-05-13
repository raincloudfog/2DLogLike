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
    }

    [SerializeField] GameObject hpiconlist;
    [SerializeField] Image Hpicon;
    public Character Player; // 플레이어 캐릭터
    [SerializeField]
    GameObject Playerbullet; // 플레이어의 총알

    Queue<GameObject> PlayerBulletPools = new Queue<GameObject>(); // 플레이어의 총알 오브젝트풀링
    Queue<Image> PlayerHpicon = new Queue<Image>(); // HP아이콘 오브젝트 풀링

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

    public void HpiconReturn(Image img)
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
}

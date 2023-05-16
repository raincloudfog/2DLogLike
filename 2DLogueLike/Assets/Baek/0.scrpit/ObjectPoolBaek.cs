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

    [SerializeField] GameObject Boom;

    [SerializeField] GameObject hpiconlist;
    [SerializeField] Image Hpicon;
    public Character Player; // �÷��̾� ĳ����
    [SerializeField]
    GameObject Playerbullet; // �÷��̾��� �Ѿ�

    Queue<GameObject> PlayerBulletPools = new Queue<GameObject>(); // �÷��̾��� �Ѿ� ������ƮǮ��
    Queue<Image> PlayerHpicon = new Queue<Image>(); // HP������ ������Ʈ Ǯ��
    Queue<GameObject> BoomPools = new Queue<GameObject>();

    public void PlayerBulletReturn(GameObject obj) // �Ѿ� ����
    {
        PlayerBulletPools.Enqueue(obj);
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.SetActive(false);
    }

    public GameObject PlayerBulletCreate() // �Ѿ� ����
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

    public void HpiconCreate() // �Ѿ� ����
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

    public GameObject BoomCreate()
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

    public void BoomReturn(GameObject obj)
    {
        BoomPools.Enqueue(obj);
        
        obj.SetActive(false);
        
        obj.transform.SetParent(null);
    }
}

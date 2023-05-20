using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBaek<GameManager>
{
    public Character Player;
    public int coin = 0;
    [SerializeField] GameObject hpiconlist;
    [SerializeField] Image Hpicon;
    [SerializeField] Text cointxt;
    public bool isUiactive = false;
    NPCKind talkingNPCKind = NPCKind.End; //�ƹ�������������~


    public Npc SHOP;// ���� ���� ��ġ
    public Npc POTION; // ���� ��ġ

    [SerializeField] int curHp;
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

        

        curHp = Player.Hp;
        if(SHOP != null && POTION != null)
        {
            Player.SHOP = SHOP;
            Player.POTION = POTION;
        }
        
    }

    public void SetIsTalking(bool _talking, NPCKind _kind)
    {
        if (_talking == true) //��ȭ �ϰ���
        {
            if (isUiactive == false)//
            {
                talkingNPCKind = _kind;
                isUiactive = true;
            }            
        }
        else //��ȭ �ݰ���
        {
            if (_kind != talkingNPCKind)
            {
                return;
            }

            isUiactive = false;
            talkingNPCKind = NPCKind.End;
        }        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BossRoom") // ���� �������ϰ��
        {
            
            for (int i = 0; i < Player.Hp; i++) // ���������� �����س� ü�¸�ŭ
            {
                ObjectPoolBaek.Instance.HpiconCreate(); // ü���� ����ϴ�.
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage") // ���������� ���
        {

            for (int i = 0; i < Player.Hp; i++) // �⺻ ü�� ��ŭ
            {
                ObjectPoolBaek.Instance.HpiconCreate(); 
            }
        }
        if (Player == null)
        {
            Player = FindObjectOfType<Character>();
            
        }
    }

    private void FixedUpdate()
    {
        cointxt.text = coin.ToString();
        
        if(Player.Hp != hpiconlist.transform.childCount)
        {
            if (Player.Hp > hpiconlist.transform.childCount)
            {                
                    ObjectPoolBaek.Instance.HpiconCreate();                
            }
            else if(Player.Hp < hpiconlist.transform.childCount)
            {
                ObjectPoolBaek.Instance.HpiconReturn
                (hpiconlist.transform.GetChild(hpiconlist.transform.childCount - 1).GetComponent<Image>());
            }
        }
    }
    private void LateUpdate()
    {
        
        if (hpiconlist == null)
        {
            hpiconlist = GameObject.Find("Hp gage");
        }
    }
}

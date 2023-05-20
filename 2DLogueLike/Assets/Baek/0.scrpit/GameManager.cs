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
    NPCKind talkingNPCKind = NPCKind.End; //아무랑도얘기안했음~


    public Npc SHOP;// 무기 상인 위치
    public Npc POTION; // 힐러 위치

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
        if (_talking == true) //대화 하겠음
        {
            if (isUiactive == false)//
            {
                talkingNPCKind = _kind;
                isUiactive = true;
            }            
        }
        else //대화 닫겠음
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
        if (SceneManager.GetActiveScene().name == "BossRoom") // 만약 보스룸일경우
        {
            
            for (int i = 0; i < Player.Hp; i++) // 스테이지에 저장해논 체력만큼
            {
                ObjectPoolBaek.Instance.HpiconCreate(); // 체력을 만듭니다.
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage") // 스테이지일 경우
        {

            for (int i = 0; i < Player.Hp; i++) // 기본 체력 만큼
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

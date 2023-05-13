using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBaek<GameManager>
{
    public Character Player;
    public int coin = 0;
    [SerializeField] GameObject hpiconlist;
    [SerializeField] Image Hpicon;
    [SerializeField] Text cointxt;

    [SerializeField] int curHp;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        curHp = Player.Hp;
    }
    private void Start()
    {
        for (int i = 0; i < Player.Hp; i++)
        {
            ObjectPoolBaek.Instance.HpiconCreate();
        }
        
    }

    private void FixedUpdate()
    {
        cointxt.text = coin.ToString();
        if(curHp > Player.Hp)
        {
            ObjectPoolBaek.Instance.HpiconReturn
                (hpiconlist.transform.GetChild(hpiconlist.transform.childCount-1).GetComponent<Image>());
            curHp = Player.Hp;
        }
        else if(curHp < Player.Hp)
        {
            curHp = Player.Hp;
            ObjectPoolBaek.Instance.HpiconCreate();
        }
        if(hpiconlist.transform.childCount == 11)
        {
            ObjectPoolBaek.Instance.HpiconReturn
                (hpiconlist.transform.GetChild(hpiconlist.transform.childCount - 1).GetComponent<Image>());
        }
    }
}

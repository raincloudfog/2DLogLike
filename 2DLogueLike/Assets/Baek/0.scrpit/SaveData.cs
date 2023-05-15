using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : SingletonBaek<SaveData>
{
    [Header("������ ü��")]
    public int Hp = 0; // ������ ü��
    [Header("������ �� Ÿ��")]
    public WaeponType waeponType; // ������ ���� ����
    public int coin = 0; // ������ ���ΰ���

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public SaveData Weapontype(WaeponType weapon) // ���� ����
    {
        waeponType = weapon;
        return this;
    }
    public SaveData Hpsave(int hp)
    {
        this.Hp = hp;
        return this;
    }
    public SaveData Coinsave(int coin)
    {
        this.coin = coin;
        return this;
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "BossRoom")
        {
            GunManager.Instance.waeponType = Instance.waeponType;
            GameManager.Instance.Player.Hp = Instance.Hp;
            GameManager.Instance.coin = Instance.coin;
        }

        
    }
}

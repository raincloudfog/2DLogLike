using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : SingletonBaek<SaveData>
{
    [Header("저장한 체력")]
    public int Hp = 0; // 저장할 체력
    [Header("저장할 총 타입")]
    public WaeponType waeponType; // 저장할 총의 종류
    public int coin = 0; // 저장할 코인갯수
    [NonReorderable]
    public bool issave = true;
    public IWeaponStrategy weaponStrategy;

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
    public SaveData Weapontype(WaeponType weapon) // 빌더 패턴
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
    public SaveData IWeapontype(IWeaponStrategy weaponStrategy)
    {
        this.weaponStrategy = weaponStrategy;
        return this;
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "BossRoom" && issave == true)
        {
            
            if (GunManager.Instance.waeponType != Instance.waeponType)
            {
                GunManager.Instance.waeponType = Instance.waeponType;
                GunManager.Instance.SetWeaponStrategy(weaponStrategy);
            }
            
            GameManager.Instance.Player.Hp = Instance.Hp;
            GameManager.Instance.coin = Instance.coin;
            issave = false;
        }
        



    }
}

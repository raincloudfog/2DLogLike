using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : SingletonBaek<SaveData>
{
    [Header("저장한 체력")]
    public int Hp = 0; // 저장할 체력
    [Header("저장할 총 타입")]
    public WeaponType WeaponType; // 저장할 총의 종류
    public int coin = 0; // 저장할 코인갯수
    [NonReorderable]
    public bool issave = true; // 만약 세이브 되었는지 확인합니다.
    public IWeaponStrategy weaponStrategy; // 무기상태확인합니다.

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
    public SaveData Weapontype(WeaponType weapon) // 빌더 패턴
    {
        WeaponType = weapon;
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
    private void FixedUpdate() // 여기다 한이유는 가끔씩 무기가 안바뀔때가있어서 fix에 넣었습니다.
    {
        if(SceneManager.GetActiveScene().name == "BossRoom" && issave == true) // 만약 보스방이면서 세이브에 데이터가 있는 경우이면
        {
            
            if (GunManager.Instance.WeaponType != Instance.WeaponType)
            {
                GunManager.Instance.WeaponType = Instance.WeaponType;
                GunManager.Instance.SetWeaponStrategy(weaponStrategy);
            }
            
            GameManager.Instance.Player.Hp = Instance.Hp;
            GameManager.Instance.coin = Instance.coin;
            issave = false; // 세이브값은 넘겨주었으니 다시 못불러오게 합니다.
        }
        



    }
}

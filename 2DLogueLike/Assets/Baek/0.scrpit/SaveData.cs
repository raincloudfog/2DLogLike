using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : SingletonBaek<SaveData>
{
    [Header("������ ü��")]
    public int Hp = 0; // ������ ü��
    [Header("������ �� Ÿ��")]
    public WeaponType WeaponType; // ������ ���� ����
    public int coin = 0; // ������ ���ΰ���
    [NonReorderable]
    public bool issave = true; // ���� ���̺� �Ǿ����� Ȯ���մϴ�.
    public IWeaponStrategy weaponStrategy; // �������Ȯ���մϴ�.

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
    public SaveData Weapontype(WeaponType weapon) // ���� ����
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
    private void FixedUpdate() // ����� �������� ������ ���Ⱑ �ȹٲ𶧰��־ fix�� �־����ϴ�.
    {
        if(SceneManager.GetActiveScene().name == "BossRoom" && issave == true) // ���� �������̸鼭 ���̺꿡 �����Ͱ� �ִ� ����̸�
        {
            
            if (GunManager.Instance.WeaponType != Instance.WeaponType)
            {
                GunManager.Instance.WeaponType = Instance.WeaponType;
                GunManager.Instance.SetWeaponStrategy(weaponStrategy);
            }
            
            GameManager.Instance.Player.Hp = Instance.Hp;
            GameManager.Instance.coin = Instance.coin;
            issave = false; // ���̺갪�� �Ѱ��־����� �ٽ� ���ҷ����� �մϴ�.
        }
        



    }
}

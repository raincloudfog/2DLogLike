using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IWeaponStrategy // 전략 패턴용도
{
    void Shoot();
}

public enum WaeponType
{
    Pistol,
    Machine_gun,
    Shotgun,
    Sniper,
    Missile,
    End
}

public class GunManager : SingletonBaek<GunManager>
{
    private IWeaponStrategy weaponStrategy; // 전략 패턴 용도
    public WaeponType waeponType = WaeponType.Pistol;
    public GameObject Gunsp; // 총 모양 바꾸기
    [SerializeField] Character Player;
    public Sprite[] Guns; // 총 종류 스프라이트
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

    public void SetWeaponStrategy(IWeaponStrategy strategy) // 전략 패턴용도
    {
        weaponStrategy = strategy;
    }



    private void FixedUpdate()
    {        
        //shoot(waeponType);
        shoot2();
    }

    void shoot2()
    {
        if (weaponStrategy != null) // 전략 패턴 용도
        {
            weaponStrategy.Shoot();
        }
    }

    public void shoot(WaeponType waeponType)
    {
        if (weaponStrategy != null) // 전략 패턴 용도
        {
            weaponStrategy.Shoot();
        }

        switch (waeponType)
        {
            case WaeponType.Pistol: // 만약 피스톨 타입이면
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[0]; // 총 스크립트는 피트톨 로
                Player.timerdelay = 0.5f; // 총딜레이 주기
                Player.Damage = 5; // 총 데미지
                Player.bulletSpeed = 25;
                break;
            case WaeponType.Machine_gun:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[1]; 
                Player.timerdelay = 0.2f;
                Player.Damage = 3;
                Player.bulletSpeed = 25;
                break;
            case WaeponType.Shotgun:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[2];
                Player.timerdelay = 1f;
                Player.Damage = 3;
                Player.bulletSpeed = 25;
                break;
            case WaeponType.Sniper:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[3];
                Player.timerdelay = 2.5f;
                Player.Damage = 15;
                Player.bulletSpeed = 30;
                break;
            case WaeponType.Missile:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[4];
                Player.timerdelay = 3.5f;
                Player.Damage = 30;
                Player.bulletSpeed = 30;
                break;
            case WaeponType.End:
                break;
            default:
                break;
        }
    }
}

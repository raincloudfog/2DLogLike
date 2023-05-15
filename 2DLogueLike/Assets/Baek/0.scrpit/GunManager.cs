using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public WaeponType waeponType = WaeponType.Pistol;
    [SerializeField] GameObject Gunsp; // �� ��� �ٲٱ�
    [SerializeField] Character Player;
    [SerializeField] Sprite[] Guns; // �� ���� ��������Ʈ
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
    

    


    private void FixedUpdate()
    {
        
        shoot(waeponType);
    }



    public void shoot(WaeponType waeponType)
    {
        switch (waeponType)
        {
            case WaeponType.Pistol: // ���� �ǽ��� Ÿ���̸�
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[0]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
                Player.timerdelay = 0.5f; // �ѵ����� �ֱ�
                Player.Damage = 5; // �� ������
                Player.bulletSpeed = 25;
                break;
            case WaeponType.Machine_gun:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[1]; 
                Player.timerdelay = 0.2f;
                Player.Damage = 3;
                Player.bulletSpeed = 50;
                break;
            case WaeponType.Shotgun:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[2];
                Player.timerdelay = 1f;
                Player.Damage = 3;
                Player.bulletSpeed = 50;
                break;
            case WaeponType.Sniper:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[3];
                Player.timerdelay = 2.5f;
                Player.Damage = 15;
                Player.bulletSpeed = 50;
                break;
            case WaeponType.Missile:
                Gunsp.GetComponent<SpriteRenderer>().sprite = Guns[4];
                Player.timerdelay = 3.5f;
                Player.Damage = 30;
                Player.bulletSpeed = 50;
                break;
            case WaeponType.End:
                break;
            default:
                break;
        }
    }
}

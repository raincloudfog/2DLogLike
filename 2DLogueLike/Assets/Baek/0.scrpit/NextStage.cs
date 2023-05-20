using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour // 다음 스테이지로 넘어가면서
{
    Collider2D Player; // 플레이어 감지

    private void FixedUpdate()
    {
        Player = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size,0,LayerMask.GetMask("Player"));
        if(Player != null)
        {
            SaveData.Instance.Weapontype(GunManager.Instance.WeaponType). // 빌더 패턴을 사용해서 세이브 데이터에 HP와 코인, 무기종류를 넘겨줍니다.
                Hpsave(Player.GetComponent<Character>().Hp).Coinsave(GameManager.Instance.coin).IWeapontype(GunManager.Instance.weaponStrategy);
            SceneManager.LoadScene(2);
        }
    }
    
}

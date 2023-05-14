using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Text text;
    bool istalking = true; // 더미 데이터
    public void OnMachine_gun()
    {
        if(GameManager.Instance.coin >= 1)
        {
            GameManager.Instance.coin -= 1;
            GunManager.Instance.waeponType = WaeponType.Machine_gun;
        }
        else
        {
            istalking = false;
            StartCoroutine(nomoney());
        }
        
    }

    public void OnShotgun()
    {
        if (GameManager.Instance.coin >= 1)
        {
            
            GameManager.Instance.coin -= 1;
            GunManager.Instance.waeponType = WaeponType.Shotgun;
        }
        else
        {
            istalking = false;
            StartCoroutine(nomoney());
        }

    }

    public void OnSniper()
    {
        if (GameManager.Instance.coin >= 2)
        {
            GameManager.Instance.coin -= 2;
            GunManager.Instance.waeponType = WaeponType.Sniper;
        }
        else
        {
            istalking = false;
            StartCoroutine(nomoney());
        }

    }

    public void OnHeal()
    {
        if (GameManager.Instance.coin >= 1)
        {
            if(GameManager.Instance.Player.Hp >= 10)
            {
                return;
            }
            GameManager.Instance.coin -= 1;
            GameManager.Instance.Player.Hp += 1;
        }
        else
        {
            istalking = false;
            StartCoroutine(nomoney());
        }
    }

    IEnumerator nomoney()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(false);
        istalking = true;
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Text text;
    bool istalking = true; // 대화 중인지 여부를 나타내는 변수

    // 기관총 버튼 클릭 시 호출되는 함수
    public void OnMachine_gun()
    {
        if (GameManager.Instance.coin >= 1) // 보유한 코인이 1 이상인 경우
        {
            GameManager.Instance.coin -= 1; // 코인 차감
            GunManager.Instance.waeponType = WaeponType.Machine_gun; // 총기 타입 변경
        }
        else // 보유한 코인이 부족한 경우
        {
            istalking = false; // 대화 중인 상태 해제
            StartCoroutine(nomoney()); // 돈이 부족하다는 메시지 출력
        }
    }

    // 샷건 버튼 클릭 시 호출되는 함수
    public void OnShotgun()
    {
        if (GameManager.Instance.coin >= 1) // 보유한 코인이 1 이상인 경우
        {
            GameManager.Instance.coin -= 1; // 코인 차감
            GunManager.Instance.waeponType = WaeponType.Shotgun; // 총기 타입 변경
        }
        else // 보유한 코인이 부족한 경우
        {
            istalking = false; // 대화 중인 상태 해제
            StartCoroutine(nomoney()); // 돈이 부족하다는 메시지 출력
        }
    }

    // 스나이퍼 버튼 클릭 시 호출되는 함수
    public void OnSniper()
    {
        if (GameManager.Instance.coin >= 2) // 보유한 코인이 2 이상인 경우
        {
            GameManager.Instance.coin -= 2; // 코인 차감
            GunManager.Instance.waeponType = WaeponType.Sniper; // 총기 타입 변경
        }
        else // 보유한 코인이 부족한 경우
        {
            istalking = false; // 대화 중인 상태 해제
            StartCoroutine(nomoney()); // 돈이 부족하다는 메시지 출력
        }
    }

    // 힐 버튼 클릭 시 호출되는 함수
    public void OnHeal()
    {
        if (GameManager.Instance.coin >= 1) // 보유한 코인이 1 이상인 경우
        {
            if (GameManager.Instance.Player.Hp >= 10) // 플레이어의 체력이 이미 최대인 경우
            {
                return; // 아무 작업 없이 함수 종료
            }
            GameManager.Instance.coin -= 1; // 코인 차감
            GameManager.Instance.Player.Hp += 1; // 플레이어 체력 증가
        }
        else // 보유한 코인이 부족한 경우
        {
            istalking = false; // 대화 중인 상태 해제
            StartCoroutine(nomoney()); // 돈이 부족하다는 메시지 출력
        }
    }

    IEnumerator nomoney() // 돈이 부족할때 메세지 출력
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(false);
        istalking = true;
        yield break;
    }
}

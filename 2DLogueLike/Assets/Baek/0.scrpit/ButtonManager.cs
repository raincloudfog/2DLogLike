using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Text text;
    bool istalking = true; // ��ȭ ������ ���θ� ��Ÿ���� ����

    // ����� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnMachine_gun()
    {
        if (GameManager.Instance.coin >= 1) // ������ ������ 1 �̻��� ���
        {
            GameManager.Instance.coin -= 1; // ���� ����
            GunManager.Instance.waeponType = WaeponType.Machine_gun; // �ѱ� Ÿ�� ����
        }
        else // ������ ������ ������ ���
        {
            istalking = false; // ��ȭ ���� ���� ����
            StartCoroutine(nomoney()); // ���� �����ϴٴ� �޽��� ���
        }
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnShotgun()
    {
        if (GameManager.Instance.coin >= 1) // ������ ������ 1 �̻��� ���
        {
            GameManager.Instance.coin -= 1; // ���� ����
            GunManager.Instance.waeponType = WaeponType.Shotgun; // �ѱ� Ÿ�� ����
        }
        else // ������ ������ ������ ���
        {
            istalking = false; // ��ȭ ���� ���� ����
            StartCoroutine(nomoney()); // ���� �����ϴٴ� �޽��� ���
        }
    }

    // �������� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnSniper()
    {
        if (GameManager.Instance.coin >= 2) // ������ ������ 2 �̻��� ���
        {
            GameManager.Instance.coin -= 2; // ���� ����
            GunManager.Instance.waeponType = WaeponType.Sniper; // �ѱ� Ÿ�� ����
        }
        else // ������ ������ ������ ���
        {
            istalking = false; // ��ȭ ���� ���� ����
            StartCoroutine(nomoney()); // ���� �����ϴٴ� �޽��� ���
        }
    }

    // �� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnHeal()
    {
        if (GameManager.Instance.coin >= 1) // ������ ������ 1 �̻��� ���
        {
            if (GameManager.Instance.Player.Hp >= 10) // �÷��̾��� ü���� �̹� �ִ��� ���
            {
                return; // �ƹ� �۾� ���� �Լ� ����
            }
            GameManager.Instance.coin -= 1; // ���� ����
            GameManager.Instance.Player.Hp += 1; // �÷��̾� ü�� ����
        }
        else // ������ ������ ������ ���
        {
            istalking = false; // ��ȭ ���� ���� ����
            StartCoroutine(nomoney()); // ���� �����ϴٴ� �޽��� ���
        }
    }

    IEnumerator nomoney() // ���� �����Ҷ� �޼��� ���
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(false);
        istalking = true;
        yield break;
    }
}

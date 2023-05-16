using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] Collider2D player;  // �÷��̾ �����ϱ� ���� �ݶ��̴�
    [SerializeField] List<GameObject> monstersList;  // ���� ������Ʈ���� ����Ʈ



    private void Awake()
    {
        // �ʱ�ȭ ��, ���� ������Ʈ���� ��Ȱ��ȭ�մϴ�.
        for (int i = 0; i < monstersList.Count; i++)
        {
            monstersList[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // ���� ������������ �÷��̾ �����մϴ�.
        player = Physics2D.OverlapBox(transform.position, transform.localScale, 0, LayerMask.GetMask("Player"));
        if (player != null)
        {
            // �÷��̾ ������ ���, ��� ���� ������Ʈ�� Ȱ��ȭ�մϴ�.
            for (int i = 0; i < monstersList.Count; i++)
            {
                monstersList[i].gameObject.SetActive(true);
            }

            // ���� ���������� ��Ȱ��ȭ�մϴ�.
            gameObject.SetActive(false);
        }
    }
}

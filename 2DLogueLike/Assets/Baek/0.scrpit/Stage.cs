using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stage : MonoBehaviour
{
    [SerializeField] Collider2D player;  // 플레이어를 감지하기 위한 콜라이더
    [SerializeField] List<GameObject> monstersList;  // 몬스터 오브젝트들의 리스트

    public GameObject bossHpObj;

    public GameObject walldoor; // 적들이 있는 맵에 들어가면 입구를 막는 벽 (혹은 문)

    private void Awake()
    {
        // 초기화 시, 몬스터 오브젝트들을 비활성화합니다.
        for (int i = 0; i < monstersList.Count; i++)
        {
            monstersList[i].SetActive(false);
        }
        if(walldoor != null) // 만약 벽이 존재하면 벽을 비활성화 해줍니다.
        {
            walldoor.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        // 현재 스테이지에서 플레이어를 감지합니다.
        player = Physics2D.OverlapBox(transform.position, transform.localScale, 0, LayerMask.GetMask("Player"));
        if (player != null)
        {
            // 플레이어를 감지한 경우, 모든 몬스터 오브젝트를 활성화합니다.
            for (int i = 0; i < monstersList.Count; i++)
            {
                monstersList[i].gameObject.SetActive(true);
            }
            if (walldoor != null) // 벽생성
            {
                walldoor.SetActive(true);
            }
            if(bossHpObj != null)
            {
                bossHpObj.SetActive(true);
            }
            
            
            // 현재 스테이지를 비활성화합니다.
            gameObject.SetActive(false);
        }
    }
}

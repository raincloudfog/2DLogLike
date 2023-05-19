using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    AudioSource bossAudio; // 보스 패턴의 사운드
    public List<AudioClip> audioClips = new List<AudioClip>();
    void Init()
    {
        bossAudio = GetComponent<AudioSource>();
    }
    public void PlayAudio(int listAudio) // 오디오클립의 리스트 안에있는
        // 소리를 재생하는 함수
    {
        Init();
        bossAudio.clip = audioClips[listAudio];
        bossAudio.Play();
    }

}

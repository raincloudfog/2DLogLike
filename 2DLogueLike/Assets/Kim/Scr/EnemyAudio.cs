using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    AudioSource bossAudio;
    public List<AudioClip> audioClips = new List<AudioClip>();
    void Init()
    {
        bossAudio = GetComponent<AudioSource>();
    }
    public void PlayAudio(int listAudio)
    {
        Init();
        bossAudio.clip = audioClips[listAudio];
        bossAudio.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBaek<AudioManager>
{
    public AudioClip[] BGMSound;
    public AudioClip[] BulletSound;
    private AudioSource audioSource;

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        setBGM();
        
    }
    private void FixedUpdate()
    {
        
        setBGM();
        if (audioSource.isPlaying == false) // 음악이 재생중이 아닐때만 재생하기
        {
            audioSource.Play();

        }



    }


    private void setBGM()
    {
        
        if(SceneManager.GetActiveScene().name == "BossRoom") // 만약 보스룸일경우 브금 변경
        {
            audioSource.clip = BGMSound[2];
        }
        else if (SceneManager.GetActiveScene().name == "Stage") // 스테이지일 경우 브금 변경
        {
            audioSource.clip = BGMSound[1];
        }
        else if(SceneManager.GetActiveScene().name == "Start") // 시작 화면일경우 브금 변경
        {
            audioSource.clip = BGMSound[0];
        }
        
    }

}

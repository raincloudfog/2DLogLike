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
            setBGM();
            audioSource.Play();
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
    }
    

    private void setBGM()
    {
        if(SceneManager.GetActiveScene().name == "BossRoom") // ���� �������ϰ�� ��� ����
        {
            audioSource.clip = BGMSound[2];
        }
        if (SceneManager.GetActiveScene().name == "Stage") // ���������� ��� ��� ����
        {
            audioSource.clip = BGMSound[1];
        }
        else // ���� ȭ���ϰ�� ��� ����
        {
            audioSource.clip = BGMSound[0];
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBaek<AudioManager>
{
    public AudioClip[] BGMSound;
    public AudioClip[] BulletSound;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

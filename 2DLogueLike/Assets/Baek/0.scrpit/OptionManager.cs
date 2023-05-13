using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : SingletonBaek<OptionManager>
{
    [SerializeField] GameObject Option;
    public GameObject died;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Option.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnRegame()
    {
        Option.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnReStart()
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnEnd()
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        Application.Quit();
    }

 
}

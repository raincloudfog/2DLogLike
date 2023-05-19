using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : SingletonBaek<OptionManager>
{
    [SerializeField] GameObject Option; // 옵션이 담긴 게임오브젝트
    public GameObject died;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc를 누를 경우 옵션이 나타납니다.
        {
            Option.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnRegame() // 게임으로 돌아갑니다.
    {
        Option.SetActive(false); 
        Time.timeScale = 1;
    }
    public void OnReStart() // 메인메뉴로 다시 돌아갑니다.
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnEnd() // 게임을 끕니다.
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        Application.Quit();
    }

 
}

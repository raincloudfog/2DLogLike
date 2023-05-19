using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : SingletonBaek<OptionManager>
{
    [SerializeField] GameObject Option; // �ɼ��� ��� ���ӿ�����Ʈ
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
        if (Input.GetKeyDown(KeyCode.Escape)) // esc�� ���� ��� �ɼ��� ��Ÿ���ϴ�.
        {
            Option.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnRegame() // �������� ���ư��ϴ�.
    {
        Option.SetActive(false); 
        Time.timeScale = 1;
    }
    public void OnReStart() // ���θ޴��� �ٽ� ���ư��ϴ�.
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnEnd() // ������ ���ϴ�.
    {
        Option.SetActive(false);
        Time.timeScale = 1;
        Application.Quit();
    }

 
}

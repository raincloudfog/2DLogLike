using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenes : MonoBehaviour
{
    public void OnStart()// ��ŸƮ �� ���������� �Ѿ�ϴ�.
    {
        SceneManager.LoadScene(1); 
    }
}

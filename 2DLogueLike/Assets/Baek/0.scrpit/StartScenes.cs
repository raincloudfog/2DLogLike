using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenes : MonoBehaviour
{
    public void OnStart()// 스타트 시 스테이지로 넘어갑니다.
    {
        SceneManager.LoadScene(1); 
    }
}

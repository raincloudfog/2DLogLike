using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    string filepath = "";
    void Start()
    {
        filepath = Path.Combine(Application.streamingAssetsPath, "test.txt");

    }
    void WriteText(string _filepath,string message) // 쓰기
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath); // streamingasset폴더가 있는지 확인부터
        if(directoryInfo.Exists == false) // 프로퍼티라서 함수가 아님
        {
            directoryInfo.Create();
        }
        using (FileStream fs = new FileStream(filepath, FileMode.Create))
        {
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8); // 한글도 가능하도록 encoding 속성을 준 것.
            sw.WriteLine("쓰고 싶은 문구 작성, 나중에 내 데이터 key와 value알아서 작성 알아서 판별해서 쓰면 될것");
            sw.Write(65); // 개행문자없음. o 줄바꿈 없음.
            sw.Write(000);
            sw.WriteLine(0.12345);
            sw.Flush();
        }
    }
    string ReadText(string _filepath)
    {
        return "";
    }
     
  
}

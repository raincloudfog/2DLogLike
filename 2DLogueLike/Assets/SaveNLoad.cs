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
    void WriteText(string _filepath,string message) // ����
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath); // streamingasset������ �ִ��� Ȯ�κ���
        if(directoryInfo.Exists == false) // ������Ƽ�� �Լ��� �ƴ�
        {
            directoryInfo.Create();
        }
        using (FileStream fs = new FileStream(filepath, FileMode.Create))
        {
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8); // �ѱ۵� �����ϵ��� encoding �Ӽ��� �� ��.
            sw.WriteLine("���� ���� ���� �ۼ�, ���߿� �� ������ key�� value�˾Ƽ� �ۼ� �˾Ƽ� �Ǻ��ؼ� ���� �ɰ�");
            sw.Write(65); // ���๮�ھ���. o �ٹٲ� ����.
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

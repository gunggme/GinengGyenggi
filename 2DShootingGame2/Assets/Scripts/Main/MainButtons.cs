using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void GameStart()
    {
        //���� �����ϱ�
        SceneManager.LoadScene(1);
    }

    public void GameQuit()
    {
        //���� ����
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //����Ǿ����� PlayerPrefs ���� ����
        PlayerPrefs.DeleteAll();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void GameStart()
    {
        //게임 시작하기
        SceneManager.LoadScene(1);
    }

    public void GameQuit()
    {
        //게임 종료
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //종료되었을대 PlayerPrefs 전부 삭제
        PlayerPrefs.DeleteAll();
    }
}

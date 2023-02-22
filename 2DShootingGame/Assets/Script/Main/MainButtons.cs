using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void StartUp()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveMain()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}

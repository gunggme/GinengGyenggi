using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void RePlay()
    {
        // move the InGame
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        // move the MainScene
        SceneManager.LoadScene(0);
    }

    public void Help()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

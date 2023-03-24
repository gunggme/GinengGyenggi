using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject back;

    public void MoveHome()
    {
        back.GetComponent<Animator>().SetTrigger("Close");
        Invoke("Home", 1);
    }

    void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void MoveStart()
    {
        back.GetComponent<Animator>().SetTrigger("Close");
        Invoke("StartGame", 1);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

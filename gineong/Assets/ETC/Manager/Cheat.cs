using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour
{
    [SerializeField] GameObject boom;

    [SerializeField] Skill skill;

    private void Update()
    {
        F1();
        F2();
        F3();
        F4();
        F5();
        F6();
    }

    void F1()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            boom.SetActive(true);
            Invoke("F1Down", 0.3f);
        }
    }

    void F1Down()
    {
        boom.SetActive(false);
    }

    void F2()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.instance.player.power = 4;
        }
    }

    void F3()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            skill.curDelay1 = 0;
            skill.curDelay2 = 0;
            skill.skill1Index = 3;
            skill.skill2Index = 2;
        }
    }

    void F4()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.instance.player.fuer = 100;
        }
    }

    void F5()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.instance.player.hp = 5;
            GameManager.instance.HPSet();
        }
    }

    void F6()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            switch(GameManager.instance.stageNum)
            {
                case 1:
                    StartCoroutine(GameManager.instance.Stage1Over());
                    break;
                case 2:
                    SceneManager.LoadScene(1);
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    [SerializeField] GameObject bom;
    [SerializeField] Skill skillS;

    private void Update()
    {
        F1();
        F2();
        F3Down();
        F4Down();
        F5Down();
        F6Down();
    }

    void F1()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            bom.gameObject.SetActive(true);
            Invoke("F1Down", 0.2f);
        }
    }

    void F1Down()
    {
        bom.gameObject.SetActive(false);
    }

    void F2()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.instance.playerS.power = 4;
        }
    }

    void F3Down()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            skillS.curSkill1 = 3;
            skillS.curSkill2 = 2;

            skillS.SkillDelay1 = 0;
            skillS.SkillDelay2 = 0;
        }
    }

    void F4Down()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.instance.playerS.fuer = 100;
        }
    }
    void F5Down()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.instance.playerS.hp = 5;
            GameManager.instance.HPSet();
        }
    }


    void F6Down()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            switch (GameManager.instance.stageNum)
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

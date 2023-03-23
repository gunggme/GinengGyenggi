using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] Player playerS;

    [SerializeField] GameObject boom;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject cheat;

    [Header("Skill1")]
    public float curSkill1Delay;
    [SerializeField] float maxSkill1Delay;
    public float skill1Index;
    [SerializeField] Text skill1Text;
    [SerializeField] Image skill1Image;

    [Header("Skill2")]
    public float skill2Index;
    public float curSkill2Delay;
    public float maxSkill2Delay;
    [SerializeField] Text skill2Text;
    [SerializeField] Image skill2Image;

    private void Update()
    {
        HPShot();
        Boom1();
    }

    void HPShot()
    {
        if(curSkill1Delay < maxSkill1Delay)
        {
            curSkill1Delay += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E))
            {
                skill1Text.gameObject.SetActive(true);
                Invoke("TextDown1", 0.35f);
            }
            return;
        }

        skill1Image.color = new Color(1, 1, 1, 1f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (skill1Index < 1)
            {
                skill1Text.gameObject.SetActive(true);
                Invoke("TextDown1", 0.4f);
            }
            skill1Index--;
            skill1Image.color = new Color(1, 1, 1, 0.5f);
            playerS.hp++;
            curSkill1Delay = 0;
            GameManager.instance.HPSet();
        }
    }

    void TextDown1()
    {
        skill1Text.gameObject.SetActive(false);
    }

    void Boom1()
    {
        if (curSkill2Delay < maxSkill2Delay)
        {
            curSkill2Delay += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                skill2Text.gameObject.SetActive(true);
                Invoke("TextDown2", 0.4f);
            }
            return;
        }

        skill1Image.color = new Color(1, 1, 1, 1);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(skill2Index < 1)
            {
                skill2Text.gameObject.SetActive(true);
                Invoke("TextDown2", 0.4f);
            }
            skill2Index--;
            skill2Image.color = new Color(1, 1, 1, 0.5f);
            boom.SetActive(true);
            curSkill2Delay = 0;
            Invoke("BoomDown", 0.3f);
        }
    }

    void TextDown2()
    {
        skill2Text.gameObject.SetActive(false);
    }

    void BoomDown()
    {
        boom.SetActive(false);
    }

    void CheatDown()
    {
        cheat.SetActive(false);
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cheat.SetActive(true);
            Invoke("CheatDown", 0.3f);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            playerS.power = 4;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            skill1Index = 3;
            skill2Index = 2;
            curSkill1Delay = 20;
            curSkill2Delay = 30;
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            playerS.hp = 5;
            GameManager.instance.HPSet();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            playerS.fuer = 100;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            switch (gameManager.stageNum)
            {
                case 1:
                    gameManager.Stage1Over();
                    break;
                case 2:
                    SceneManager.LoadScene(1);
                    break;
            }
        }
    }
}

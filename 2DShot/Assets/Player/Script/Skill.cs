using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Skill1")]
    [SerializeField] Image skill1Image;
    [SerializeField] Slider skill1Slider;
    public float SkillDelay1;
    [SerializeField] Text skill1Text;
    public int curSkill1;
    [SerializeField] Text skillText1;

    [Header("Skill2")]
    [SerializeField] Image skill2Image;
    [SerializeField] Slider skill2Slider;
    [SerializeField] Text skill2Text;
    public float SkillDelay2;
    public int curSkill2;
    [SerializeField] Text skillText2;
    [SerializeField] GameObject boom;

    private void Update()
    {
        SkillE();
        SkillR();
    }

    void SkillE()
    {
        skill1Slider.value = SkillDelay1 / 20;
        skillText1.text = curSkill1.ToString();

        if(curSkill1 > 0)
        {
            if (SkillDelay1 >= 0)
            {
                SkillDelay1 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //아직 사용이 불가능합니다.
                    skill1Text.gameObject.SetActive(true);
                    Invoke("TextDown1", 0.5f);
                }
                return;
            }

            
        }
        else if(curSkill1 <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //아직 사용이 불가능합니다.
                skill1Text.gameObject.SetActive(true);
                Invoke("TextDown1", 0.5f);
            }
            return;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillDelay1 = 20;
            curSkill1--;
            GameManager.instance.playerS.hp += 2;
            GameManager.instance.HPSet();
        }
    }
    void TextDown1()
    {
        skill1Text.gameObject.SetActive(false);
    }

    void SkillR()
    {
        skill2Slider.value = SkillDelay2 / 30;
        skillText2.text = curSkill2.ToString();

        if (curSkill2 > 0)
        {
            if (SkillDelay2 >= 0)
            {
                SkillDelay2 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //아직 사용이 불가능합니다.
                    skill2Text.gameObject.SetActive(true);
                    Invoke("TextDown2", 0.5f);
                }
                return;
                
            }


        }
        else if (curSkill2 <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //아직 사용이 불가능합니다.
                skill2Text.gameObject.SetActive(true);
                Invoke("TextDown2", 0.5f);
            }
            return;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            boom.SetActive(true);
            Invoke("BoomDown", 0.2f);
            SkillDelay2 = 30;
            curSkill2--;
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
}

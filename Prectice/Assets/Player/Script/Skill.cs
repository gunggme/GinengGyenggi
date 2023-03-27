using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Skill1")]
    [SerializeField] float curSkillDelay1;
    [SerializeField] int skillIndex1;
    [SerializeField] Text skill1DontPlay;
    [SerializeField] Text index1;
    [SerializeField] Slider skill1Slider;
    [Header("Skill2")]
    [SerializeField] float curSkillDelay2;
    [SerializeField] int skillIndex2;
    [SerializeField] GameObject boom;
    [SerializeField] Text skill2DontPlay;
    [SerializeField] Text index2;
    [SerializeField] Slider skill2Slider;

    private void Update()
    {
        Skill1();
        Skill2();
    }


    void Skill1()
    {
        index1.text = skillIndex1.ToString();
        if(skillIndex1 > 0)
        {
            if(curSkillDelay1 > 0)
            {
                skill1Slider.value = curSkillDelay1 / 20;
                curSkillDelay1 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //아직 사용 불가능 하다는걸 알림
                    skill1DontPlay.gameObject.SetActive(true);
                    Invoke("TextDown1", 0.5f);
                }
                return;
            }
            
        }
        else if(skillIndex1 <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //아직 사용 불가능 하다는걸 알림
                skill1DontPlay.gameObject.SetActive(true);
                Invoke("TextDown1", 0.5f);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            curSkillDelay1 = 20;
            GameManager.instance.player.hp++;
            GameManager.instance.HPSet();
            skillIndex1--;
        }
    }

    void TextDown1()
    {
        skill1DontPlay.gameObject.SetActive(false);
    }

    void Skill2()
    {
        index2.text = skillIndex2.ToString();
        if (skillIndex2 > 0)
        {
            if (curSkillDelay2 > 0)
            {
                skill2Slider.value = curSkillDelay2 / 30;
                curSkillDelay2 -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //아직 사용 불가능 하다는걸 알림
                    skill2DontPlay.gameObject.SetActive(true);
                    Invoke("TextDown2", 0.5f);
                }
                return;
            }
        }
        else if (skillIndex1 <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //아직 사용 불가능 하다는걸 알림
                skill2DontPlay.gameObject.SetActive(true);
                Invoke("TextDown2", 0.5f);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            curSkillDelay2 = 30;
            boom.gameObject.SetActive(true);
            Invoke("BoomDown", 0.3f);
            skillIndex2--;
        }
    }

    void TextDown2()
    {
        skill2DontPlay.gameObject.SetActive(false);
    }

    void BoomDown()
    {
        boom.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Skill1")]
    [SerializeField] public float curDelay1 = 0;
    [SerializeField] public int skill1Index;
    [SerializeField] Slider skill1Slider;
    [SerializeField] Text dont1;
    [SerializeField] Text index1;

    [Header("Skill2")]
    [SerializeField] public float curDelay2 = 0;
    [SerializeField] public int skill2Index;
    [SerializeField] Slider skill2Slider;
    [SerializeField] Text dont2;
    [SerializeField] Text index2;
    [SerializeField] GameObject boom;

    private void Update()
    {
        Skill1();
        Skill2();
    }

    void Skill1()
    {
        index1.text = index1.ToString();

        if(skill1Index == 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dont1.gameObject.SetActive(true);
            }
            return;
        }
        if(curDelay1 > 0)
        {
            skill1Slider.value = curDelay1 / 20;
            curDelay1 -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E))
            {
                dont1.gameObject.SetActive(true);
                Invoke("TextDown1", 0.5f);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.player.hp++;
            GameManager.instance.HPSet();
            curDelay1 = 20;
        }
    }

    void TextDown1()
    {
        dont1.gameObject.SetActive(false);
    }

    void Skill2()
    {
        index2.text = index2.ToString();
        if (skill2Index == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                dont2.gameObject.SetActive(true);
            }
            return;
        }
        if (curDelay2 > 0)
        {
            skill1Slider.value = curDelay2 / 30;
            curDelay2 -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                dont2.gameObject.SetActive(true);
                Invoke("TextDown2", 0.5f);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            boom.SetActive(true);
            Invoke("BoomFalse", 0.3f);
            curDelay2 = 30;
        }
    }

    void BoomFalse()
    {
        boom.SetActive(false);
    }

    void TextDown2()
    {
        dont2.gameObject.SetActive(false);
    }
}

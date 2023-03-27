using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject rank;

    [SerializeField] Text[] rankText;
    [SerializeField] RectTransform ranking;

    [SerializeField] float[] rankTime;
    [SerializeField] float[] rankScore;
    [SerializeField] string[] rankName;

    bool isMove;

    private void Awake()
    {
        rankName = new string[10];
        rankScore = new float[10];
        rankTime = new float[10];

        for (int i = 0; i < 10; i++)
        {
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");

            rankText[i].text = "Rank " + (i + 1) + ". " + rankName[i] + "\nScore : " + rankScore[i].ToString("N0") + "\nTime : " + rankTime[i]; 
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpRanked();
    }

    void Main()
    {
        main.GetComponent<Animator>().SetTrigger("Open");
    }

    public void RankOpen()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        isMove = true;
        ranking.anchoredPosition = new Vector2(0, -420);
        Invoke("Rank", 0.7f);
    }

    public void RankClose()
    {
        rank.GetComponent<Animator>().SetTrigger("Close");
        isMove = false;
        Invoke("Main", 0.7f);
    }
    void Rank()
    {
        rank.GetComponent<Animator>().SetTrigger("Open");
    }

    void UpRanked()
    {
        if (isMove)
        {
            ranking.anchoredPosition += Vector2.up * 60 * Time.deltaTime;
            if(ranking.anchoredPosition.y > 1900)
            {
                ranking.anchoredPosition = new Vector2(0, -420);
            }
        }
    }
}

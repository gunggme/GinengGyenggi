using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject Rank;

    [SerializeField] GameObject rankText;
    [SerializeField] Text[] ranking;

    float[] rankScore;
    string[] rankName;

    bool isMoving;

    private void Awake()
    {
        rankScore = new float[10];
        rankName = new string[10];
       //ranking = new Text[10];

        for(int i = 0; i < ranking.Length; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            ranking[i].text = "Rank " + (i + 1) + rankName[i] + "\nScore : " + rankScore[i].ToString("N0");
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            rankText.GetComponent<RectTransform>().anchoredPosition += Vector2.up * 50 * Time.deltaTime;
            if(rankText.GetComponent<RectTransform>().anchoredPosition.y == 1220)
            {
                rankText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -660);
            }
        }
    }

    public void CloseRank()
    {
        Rank.GetComponent<Animator>().SetTrigger("Close");
        Invoke("MainOn", 0.5f);
        isMoving = false;
    }

    void MainOn()
    {
        main.GetComponent<Animator>().SetTrigger("Open");
    }

    public void OpenRank()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        Invoke("RankOn", 0.5f);
    }

    void RankOn()
    {
        Rank.GetComponent<Animator>().SetTrigger("Open");
        rankText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -660);
        isMoving = true;
    }
}

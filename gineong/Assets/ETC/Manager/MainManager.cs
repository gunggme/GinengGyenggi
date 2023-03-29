using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Text[] rankText; 

    [SerializeField] float[] rankScore;
    [SerializeField] float[] rankTime;
    [SerializeField] string[] rankName;

    [SerializeField] GameObject main;
    [SerializeField] GameObject rank;

    [SerializeField] RectTransform rankObj;

    bool isMove;

    private void Awake()
    {
        rankScore = new float[10];
        rankTime = new float[10];
        rankName = new string[10];
    }

    private void OnEnable()
    {
        for(int i = 0; i < 10; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
        }
    }

    private void Start()
    {
        for (int i = 0; i < rankText.Length; i++)
        {
            rankText[i].text = "Rank " + (i + 1) + ". " + rankName[i] + "\nScore : " + rankScore[i].ToString("N0") + "\nTime : " + rankTime[i];
        }
    }

    private void Update()
    {
        if (isMove)
        {
            rankObj.anchoredPosition += Vector2.up * 60 * Time.deltaTime;
            if(rankObj.anchoredPosition.y >= 1847)
            {
                rankObj.anchoredPosition = new Vector2(0, -560);
            }
        }
    }

    public void RankOpen()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        rankObj.anchoredPosition = new Vector2(0, -560);
        isMove = true; ;
        Invoke("Rank", 1);
    }

    public void RankClose()
    {
        rank.GetComponent<Animator>().SetTrigger("Close");
        isMove = false;
        Invoke("Main", 1);
    }

    void Rank()
    {
        rank.GetComponent<Animator>().SetTrigger("Open");
    }

    void Main()
    {
        main.GetComponent<Animator>().SetTrigger("Open");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EXIT()
    {
        Application.Quit();
    }
}

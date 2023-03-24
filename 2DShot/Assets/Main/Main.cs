using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject rank;
    [SerializeField] GameObject howToPlay;

    [SerializeField] GameObject rankTexts;

    [SerializeField] float[] rankTime;
    [SerializeField] float[] rankScore;
    [SerializeField] string[] rankName;

    [SerializeField] Text[] rankText;

    bool isMove;

    private void Awake()
    {
        rankTime = new float[10];
        rankScore = new float[10];
        rankName = new string[10];
    }

    private void Start()
    {
        SetRank();
    }

    private void Update()
    {
        if (isMove)
        {
            rankTexts.GetComponent<RectTransform>().anchoredPosition += Vector2.down * 40 * Time.deltaTime;
            if(rankTexts.GetComponent<RectTransform>().anchoredPosition.y >= 1794)
            {
                rankTexts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -770);
            }
        }
    }

    void SetRank()
    {
        for(int i = 0; i < 10; i++)
        {
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTimer");
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            rankText[i].text = "Rank " + (i + 1) + "\nScore : " + rankScore[i] + "\nTime : " + rankTime[i];
        }
    }

    public void MoveHow()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        Invoke("HowToPlay", 0.7f);
    }
    public void HowClose()
    {
        howToPlay.GetComponent<Animator>().SetTrigger("Close");
        Invoke("Mainmenu", 0.7f);
    }

    void HowToPlay()
    {
        howToPlay.GetComponent<Animator>().SetTrigger("Open");
    }

    public void MoveGames()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        Invoke("MoveGame", 1);
    }

    void MoveGame()
    {
        SceneManager.LoadScene(1);
    }

    void Mainmenu()
    {
        main.GetComponent<Animator>().SetTrigger("Open");
    }

    public void OpenRank()
    {
        main.GetComponent<Animator>().SetTrigger("Close");
        rankTexts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -770);
        isMove = true;
        Invoke("Rank", 0.7f);
    }

    public void RankClose()
    {
        rank.GetComponent<Animator>().SetTrigger("Close");
        isMove = false;
        Invoke("Mainmenu", 0.7f);
    }

    void Rank()
    {
        rank.GetComponent<Animator>().SetTrigger("Open");
    }
}

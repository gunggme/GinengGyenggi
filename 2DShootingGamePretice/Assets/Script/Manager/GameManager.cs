using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player playerS;
    [Header("Player HP")]
    [SerializeField] Slider playerHPBar;
    [SerializeField] Text playerHPText;

    [Header("Sick")]
    [SerializeField] Slider sickBar;
    [SerializeField] Text sickText;
    [SerializeField] public float sick;

    [Header("Score")]
    [SerializeField] public int score;
    [SerializeField] Text scoreText;

    private void Update()
    {
        HPSet();
        ScoreSet();
        SickSet();
        GameOver();
    }

    void HPSet()
    {
        playerHPBar.value = playerS.hp / 30;
        playerHPText.text = "Player HP : " + playerS.hp;
    }

    void ScoreSet()
    {
        scoreText.text = "Score : " + score;
    }

    void GameOver()
    {
        if(playerHPBar.value == 0)
        {
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("curScore", score);
        }
        if(sickBar.value >= 1)
        {
            Time.timeScale = 0;
            PlayerPrefs.SetInt("curScore", score);
        }
    }

    void SickSet()
    {
        sickBar.value = sick / 100;
        sickText.text = "Sick : " + sick; 
    }
}

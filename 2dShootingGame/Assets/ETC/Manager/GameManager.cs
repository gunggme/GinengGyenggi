using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] public float score;
    [SerializeField] Text scoreText;

    [Header("PlayerHP")]
    [SerializeField] Player playerS;
    [SerializeField] Slider hpBar;

    [SerializeField] Slider fuerBar;

    private void Update()
    {
        SetScore();
        SetHPBar();
        SetFuer();
    }

    void SetScore()
    {
        scoreText.text = "Score : " + score.ToString("N0");
    }

    void SetHPBar()
    {
        hpBar.value = playerS.hp / 40;
    }

    void SetFuer()
    {
        fuerBar.value = playerS.fuer / 100;
    }
}

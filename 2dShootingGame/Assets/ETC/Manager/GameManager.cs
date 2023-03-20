using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectManager objMana;

    [Header("Player")]
    public Player playerS;
    [SerializeField] Image[] hpImage;

    [Header("Score")]
    public float score;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetScore()
    {
        scoreText.text = "Score : " + score.ToString("N0");
    }

    public void PlayaerHP()
    {
        switch (playerS.hp)
        {
            case 5:
                hpImage[4].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[0].color = new Color(1, 1, 1, 1);
                break;
            case 4:
                hpImage[4].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[0].color = new Color(1, 1, 1, 0);
                break;
            case 3:
                hpImage[4].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[0].color = new Color(1, 1, 1, 0);
                break;
            case 2:
                hpImage[4].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[0].color = new Color(1, 1, 1, 0);
                break;
            case 1:
                hpImage[4].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[0].color = new Color(1, 1, 1, 0);
                break;
            case 0:
                hpImage[4].color = new Color(1, 1, 1, 0);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[0].color = new Color(1, 1, 1, 0);
                break;
        }
    }
}

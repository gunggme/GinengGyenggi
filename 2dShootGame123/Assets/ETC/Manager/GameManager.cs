using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player playerS;
    [SerializeField] Slider playerDurabilityBar;
    [SerializeField] Slider playerOilBar;

    [Header("Score")]
    [SerializeField] public float score;


    private void Update()
    {
        setDurability();
        oilSet();
    }

    void setDurability()
    {
        playerDurabilityBar.value = playerS.hp / 30;
    }

    void oilSet()
    {
        playerOilBar.value = playerS.oil / 100;
    }
}

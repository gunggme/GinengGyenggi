using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;
    [Header("Score")]
    public float score;

    [Header("Stage")]
    public int stageNum;

    private void Awake()
    {
        instance = this;
    }
}

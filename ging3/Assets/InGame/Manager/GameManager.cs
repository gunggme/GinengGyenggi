using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;

    public SpawnManager spawnMana;

    public Player player;

    [Header("Stage")]
    public int stageNum;

    [Header("Score")]
    public float score;
    [SerializeField] Text stageText;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("StageStart");
    }

    IEnumerator StageStart()
    {
        stageNum++;
        stageText.text = "Stage " + stageNum + "\nStart!";
        stageText.gameObject.SetActive(true);
        spawnMana.isSpawn = true;
        yield return new WaitForSeconds(0.6f);
        stageText.gameObject.SetActive(false);
    }
}

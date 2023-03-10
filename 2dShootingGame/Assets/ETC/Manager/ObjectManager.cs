using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("BossBullet")]
    [SerializeField] GameObject bossBullet1Prefab;
    [SerializeField] GameObject bossBullet2Prefab;
    [Header("Item")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] GameObject fuerPrefab;
    [Header("Enemy")]
    [SerializeField] GameObject enemySPrefab;
    [SerializeField] GameObject enemyMPrefab;
    [SerializeField] GameObject enemyLPrefab;
    [SerializeField] GameObject meteoPrefab;
    [Header("EnemyBullet")]
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    [Header("PlayerBullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //BossBullet
    GameObject[] bossBullet1;
    GameObject[] bossBullet2;
    //Item
    GameObject[] power;
    GameObject[] hp;
    GameObject[] fuer;
    GameObject[] coin;
    //Enemy
    GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;
    GameObject[] meteo;
    //EnemyBullet
    GameObject[] enemyBullet1;
    GameObject[] enemyBullet2;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;

    private void Awake()
    {
        //BossBullet
        bossBullet1 = new GameObject[100];
        bossBullet2 = new GameObject[100];
        //Item
        power = new GameObject[20];
        hp = new GameObject[20];
        fuer = new GameObject[20];
        coin = new GameObject[20];
        //Enemy
        enemyS = new GameObject[25];
        enemyM = new GameObject[25];
        enemyL = new GameObject[25];
        meteo = new GameObject[25];
        //enemyBullet
        enemyBullet1 = new GameObject[100];
        enemyBullet2 = new GameObject[100];
        //PlayerBullet
        playerBullet1 = new GameObject[100]; 
        playerBullet2 = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        //BossBullet
        for(int i = 0; i < bossBullet1.Length; i++)
        {
            bossBullet1[i] = Instantiate(bossBullet1Prefab);
            bossBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < bossBullet2.Length; i++)
        {
            bossBullet2[i] = Instantiate(bossBullet2Prefab);
            bossBullet2[i].gameObject.SetActive(false);
        }
        //Item
        for (int i = 0; i < coin.Length; i++)
        {
            coin[i] = Instantiate(coinPrefab);
            coin[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < power.Length; i++)
        {
            power[i] = Instantiate(powerPrefab);
            power[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < hp.Length; i++)
        {
            hp[i] = Instantiate(hpPrefab);
            hp[i].gameObject.SetActive(false);
        }
        for(int i = 0; i <fuer.Length; i++)
        {
            fuer[i] = Instantiate(fuerPrefab);
            fuer[i].gameObject.SetActive(false);
        }
        //Enemy
        for(int i  = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].gameObject.SetActive(false);
        }
        for(int i = 0; i <enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < meteo.Length; i++)
        {
            meteo[i] = Instantiate(meteoPrefab);
            meteo[i].gameObject.SetActive(false);
        }
        //EnemyBullet
        for(int i = 0; i < enemyBullet1.Length; i++)
        {
            enemyBullet1[i] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyBullet2.Length; i++)
        {
            enemyBullet2[i] = Instantiate(enemyBullet2Prefab);
            enemyBullet2[i].gameObject.SetActive(false);
        }
        //PlayerBullet
        for(int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < playerBullet2.Length; i++)
        {
            playerBullet2[i] = Instantiate(playerBullet2Prefab);
            playerBullet2[i].gameObject.SetActive(false);
        }
    }

    public GameObject MakeObj(string name)
    {
        switch (name)
        {
            //BossBullet
            case "BossBullet1":
                targetPool = bossBullet1;
                break;
            case "BossBullet2":
                targetPool = bossBullet2;
                break;
            //Item
            case "Coin":
                targetPool = coin;
                break;
            case "Power":
                targetPool = power;
                break;
            case "Fuer":
                targetPool = power;
                break;
            case "HP":
                targetPool = hp;
                break;
            //Enemy
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "Meteo":
                targetPool = meteo;
                break;
            //EnemyBullet
            case "EnemyBullet1":
                targetPool = enemyBullet1;
                break;
            case "EnemyBullet2":
                targetPool = enemyBullet2;
                break;
            //PlayerBullet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
                break;
        }

        for(int i = 0; i < targetPool.Length;i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].gameObject.SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}

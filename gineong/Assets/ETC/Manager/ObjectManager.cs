using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject mzPrefab;
    [SerializeField] GameObject fuerPrefab;
    [SerializeField] GameObject hpPrefab;
    [Header("Enem")]
    [SerializeField] GameObject enemSPrefab;
    [SerializeField] GameObject enemMPrefab;
    [SerializeField] GameObject enemLPrefab;
    [SerializeField] GameObject meteor1Prefab;
    [SerializeField] GameObject meteor2Prefab;
    [Header("EnemBullet")]
    [SerializeField] GameObject enemBullet1Prefab;
    [SerializeField] GameObject enemBullet2Prefab;
    [SerializeField] GameObject enemBullet3Prefab;
    [Header("PlayerBullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //Item
    GameObject[] coin;
    GameObject[] mz;
    GameObject[] power;
    GameObject[] hp;
    GameObject[] fuer;
    //Enem
    GameObject[] enemS;
    GameObject[] enemM;
    GameObject[] enemL;
    GameObject[] meteor1;
    GameObject[] meteor2;
    //EnemBullet
    GameObject[] enemBullet1;
    GameObject[] enemBullet2;
    GameObject[] enemBullet3;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;

    private void Awake()
    {
        coin = new GameObject[20];
        mz = new GameObject[20];
        power = new GameObject[20];
        fuer = new GameObject[20];
        hp = new GameObject[20];
        //Enem
        enemS = new GameObject[20];
        enemM = new GameObject[20];
        enemL = new GameObject[20];
        meteor1 = new GameObject[20];
        meteor2 = new GameObject[20];
        //EnemBullet
        enemBullet1 = new GameObject[200];
        enemBullet2 = new GameObject[200];
        enemBullet3 = new GameObject[200];
        //PlayerBullet
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];

        Gen();
    }

    void Gen()
    {
        //Item
        for(int i =0; i < coin.Length; i++)
        {
            coin[i] = Instantiate(coinPrefab);
            coin[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < power.Length; i++)
        {
            power[i] = Instantiate(powerPrefab);
            power[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < mz.Length; i++)
        {
            mz[i] = Instantiate(mzPrefab);
            mz[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < fuer.Length; i++)
        {
            fuer[i] = Instantiate(fuerPrefab);
            fuer[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = Instantiate(hpPrefab);
            hp[i].gameObject.SetActive(false);
        }
        //Enem
        for (int i = 0; i < enemS.Length; i++)
        {
            enemS[i] = Instantiate(enemSPrefab);
            enemS[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemM.Length; i++)
        {
            enemM[i] = Instantiate(enemMPrefab);
            enemM[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemL.Length; i++)
        {
            enemL[i] = Instantiate(enemLPrefab);
            enemL[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < meteor1.Length; i++)
        {
            meteor1[i] = Instantiate(meteor1Prefab);
            meteor1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < meteor2.Length; i++)
        {
            meteor2[i] = Instantiate(meteor2Prefab);
            meteor2[i].gameObject.SetActive(false);
        }
        //EnemBullet
        for (int i = 0; i < enemBullet1.Length; i++)
        {
            enemBullet1[i] = Instantiate(enemBullet1Prefab);
            enemBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet2.Length; i++)
        {
            enemBullet2[i] = Instantiate(enemBullet2Prefab);
            enemBullet2[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemBullet3.Length; i++)
        {
            enemBullet3[i] = Instantiate(enemBullet3Prefab);
            enemBullet3[i].gameObject.SetActive(false);
        }
        //PlayerBullet
        for (int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < playerBullet2.Length; i++)
        {
            playerBullet2[i] = Instantiate(playerBullet2Prefab);
            playerBullet2[i].gameObject.SetActive(false);
        }
    }

    public GameObject MakeObj(string names)
    {
        switch(names)
        {
            //Item
            case "Coin":
                targetPool = coin;
                break;
            case "Power":
                targetPool = power;
                break;
            case "MZ":
                targetPool = mz;
                break;
            case "Fuer":
                targetPool = fuer;
                break;
            case "HP":
                targetPool = hp;
                break;
            //Enem
            case "EnemS":
                targetPool = enemS;
                break;
            case "EnemM":
                targetPool = enemM;
                break;
            case "EnemL":
                targetPool = enemL;
                break;
            case "Meteor1":
                targetPool = meteor1;
                break;
            case "Meteor2":
                targetPool = meteor2;
                break;
            //EnemBullet
            case "EnemBullet1":
                targetPool = enemBullet1;
                break;
            case "EnemBullet2":
                targetPool = enemBullet2;
                break;
            case "EnemBullet3":
                targetPool = enemBullet3;
                break;
            //PlayerBullet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].gameObject.activeSelf)
            {
                targetPool[i].gameObject.SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}

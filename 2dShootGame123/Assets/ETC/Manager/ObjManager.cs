using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [Header("EnemyBullet")]
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    [Header("Player Bullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //EnemyBullet
    GameObject[] enemyBullet1;
    GameObject[] enemyBullet2;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;


    GameObject[] targetPool;

    private void Awake()
    {
        //EnemyBullet
        enemyBullet1 = new GameObject[50];
        enemyBullet2 = new GameObject[50];
        //PlayerBullet
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        //EnemyBullet
        for(int i = 0; i < enemyBullet1.Length; i++)
        {
            enemyBullet1[i] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyBullet2.Length; i++)
        {
            enemyBullet2[i] = Instantiate(enemyBullet2Prefab);
            enemyBullet2[i].gameObject.SetActive(false);
        }
        //PlayerBullet
        for (int i = 0; i < playerBullet1.Length; i++)
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

        for(int i = 0; i < targetPool.Length; i++)
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

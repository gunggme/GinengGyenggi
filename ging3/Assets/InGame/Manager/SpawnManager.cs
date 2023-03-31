using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField] string[] enemNames;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [SerializeField] int ranTran;

    public bool isSpawn;

    private void Awake()
    {
        enemNames = new string[] { "EnemS", "EnemM", "EnemL", "EnemS", "EnemM", "Meteor1", "Meteor1", "Meteor1", "Meteor2", "Meteor2" };
    }

    private void Start()
    {
        isSpawn = true;
    }

    void Update()
    {
        if (isSpawn)
        {
            SpawnWait();
        }    
    }

    void SpawnWait()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        Spawn();
        curDelay = 0;
        maxDelay = Random.Range(0.7f, 1.3f);
    }

    void Spawn()
    {
        switch (GameManager.instance.stageNum)
        {
            case 1:
                ranTran = Random.Range(0, spawnPoints.Length - 4);
                break;
            case 2:
                ranTran = Random.Range(0, spawnPoints.Length);
                break;
        }

        int ranEnem = Random.Range(0, enemNames.Length);
        GameObject dir = GameManager.instance.objMana.MakeObj(enemNames[ranEnem]);
        dir.transform.position = spawnPoints[ranTran].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
        Enemis enem = dir.GetComponent<Enemis>();
        if(ranTran == 5 || ranTran == 6)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enem.speed - 1, -1);
        }
        else if(ranTran == 7 || ranTran == 8)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enem.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enem.speed * -1);
        }
    }


}

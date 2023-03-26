using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    [SerializeField]
    string[] enemNames;

    [SerializeField] int ranTran;

    public bool isSpawn;

    private void Awake()
    {
        enemNames = new string[] { "EnemS", "Meteor1", "EnemS", "Meteor1", "EnemM", "Meteor1", "EnemS", "Meteor1", "EnemL", "Meteor1", "EnemS", "Meteor1", "Meteor2" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
    {
        if (isSpawn)
        {
            if (curDelay < nextDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }

            Spawn();
            curDelay = 0;
            nextDelay = Random.Range(0.9f, 1.5f);
        }
    }

    void Spawn()
    {
        int ranEnem = Random.Range(0, enemNames.Length);

        switch (GameManager.instance.stageNum)
        {
            case 1:
                ranTran = Random.Range(0, spawnPoint.Length - 4);
                break;
            case 2:
                ranTran = Random.Range(0, spawnPoint.Length);
                break;
        }

        GameObject enem = GameManager.instance.objMana.MakeObj(enemNames[ranEnem]);

        enem.transform.position = spawnPoint[ranTran].transform.position;

        Enemis enemLogic = enem.GetComponent<Enemis>();
        Rigidbody2D rigid = enem.GetComponent<Rigidbody2D>();
        if(ranTran == 5 || ranTran == 6)
        {
            enem.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemLogic.speed * -1, -1);
        }
        else if(ranTran == 7 || ranTran == 8)
        {
            enem.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemLogic.speed * -1);
        }
    }
}

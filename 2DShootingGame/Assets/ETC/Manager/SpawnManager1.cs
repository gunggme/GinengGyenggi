using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] string[] names;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    private void Awake()
    {
        names = new string[] { "EnemS", "EnemM", "EnemL", "Meteo1", "Meteo2" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
    {
        if(curDelay < nextDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        curDelay = 0;
        nextDelay = Random.Range(0.7f, 1.3f);
        Spawn();
    }

    void Spawn()
    {
        int ranTran = Random.Range(0, spawnPoints.Length);
        int ranEnem = Random.Range(0, names.Length);

        GameObject dir = GameManager.Instance.objMana.MakeObj(names[ranEnem]);

        dir.transform.position = spawnPoints[ranTran].position;

        //적 로직 가져오기
        Enemis enem = dir.GetComponent<Enemis>();

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, enem.speed * (-1));
    }
}

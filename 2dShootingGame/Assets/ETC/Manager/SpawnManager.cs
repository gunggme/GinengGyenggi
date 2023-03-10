using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    [SerializeField] string[] name;

    [SerializeField] float speed;

    [SerializeField] ObjectManager objMana;

    private void Awake()
    {
        name = new string[] { "EnemyS", "EnemyM", "EnemyL", "Meteo" };
    }

    private void Update()
    {
        SpawnDelay();
    }

    void SpawnDelay()
    {
        if(curDelay < nextDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        curDelay = 0;
        nextDelay = Random.Range(0.5f, 1.2f);
        Spawn();
    }

    void Spawn()
    {
        int ranEnem = Random.Range(0, name.Length);
        int ranTran = Random.Range(0, spawnPoints.Length);

        GameObject dir = objMana.MakeObj(name[ranEnem]);
        dir.transform.position = spawnPoints[ranTran].transform.position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        if (dir.CompareTag("Enemy"))
        {
            Enemis enem = dir.GetComponent<Enemis>();
            speed = enem.speed;
        }

        if(ranTran == 4 || ranTran == 5)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(speed * (-1), -1);
        }
        else if(ranTran == 6 || ranTran == 7)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, speed * (-1));
        }
    }
}

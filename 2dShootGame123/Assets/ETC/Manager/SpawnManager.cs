using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("ETC")]
    [SerializeField] Transform[] spawns;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;
    [SerializeField] float speed;
    [Header("Manager")]
    [SerializeField] ObjManager objMana;

    string[] name;

    private void Awake()
    {
        name = new string[] { "EnemyS", "EnemyM", "EnemyL", "Meteo" };
    }

    private void Update()
    {
        SpawnReady();
    }

    void SpawnReady()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }


        Spawn();
        curDelay = 0;
        maxDelay = Random.Range(0.5f, 1f);

    }

    void Spawn()
    {
        int randomEnemy = Random.Range(0, name.Length);
        int randomTran = Random.Range(0, spawns.Length);

        GameObject dir = objMana.MakeObj(name[randomEnemy]);
        dir.transform.position = spawns[randomTran].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        Enemy enem = dir.GetComponent<Enemy>();
        speed = enem.speed;


        if(randomTran == 4 || randomTran == 5)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(speed * (-1), -1);
        }
        else if(randomTran == 6 || randomTran == 7)
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

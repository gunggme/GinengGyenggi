using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [Header("EnemyName")]
    [SerializeField] string name;

    [Header("EnemyStet")]
    float hp;
    float dmg;
    float speed;
    float curDelay;
    float maxDelay;

    ObjectManager objMana;
    Transform playerT;

    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "EnemyS":
                hp = 5;
                dmg = 4;
                speed = 3;
                maxDelay = 1;
                break;
            case "EnemySs":
                hp = 5;
                dmg = 4;
                speed = 5;
                maxDelay = 0.8f;
                break;
            case "EnemyM":
                hp = 7;
                dmg = 6;
                speed = 10;
                break;
            case "EnemyMs":
                hp = 7;
                dmg = 6;
                speed = 8;
                maxDelay = 0.7f;
                break;
            case "EnemyL":
                hp = 15;
                dmg = 8;
                speed = 1;
                maxDelay = 1;
                break;
        }
    }

    private void Update()
    {
        Shot();
    }

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if(name == "EnemyS")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet1");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 7, ForceMode2D.Impulse);
        }
        else if (name == "EnemySs")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 7, ForceMode2D.Impulse);
        }
        else if (name == "EnemyMs")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 8, ForceMode2D.Impulse);
        }
        else if (name == "EnemyL")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            GameObject dir2 = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;
            dir2.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            Bullet b2 = dir2.GetComponent<Bullet>();
            b.dmg = dmg;
            b2.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = dir2.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 7, ForceMode2D.Impulse);
            rigid2.AddForce(pos.normalized * 7, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }
}

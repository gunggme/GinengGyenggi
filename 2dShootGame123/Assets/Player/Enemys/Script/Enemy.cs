using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] string name;

    [Header("Enemy Stats")]
    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] float dmg;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    ObjManager objMana;
    Transform playerT;

    private void Awake()
    {
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjManager>();
        playerT = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "Meteo":
                hp = 10;
                speed = 5;
                dmg = 5;
                break;
            case "S":
                hp = 5;
                speed = 3;
                dmg = 6;
                maxDelay = 1;
                break;
            case "M":
                hp = 10;
                speed = 10;
                dmg = 10;
                break;
            case "L":
                hp = 20;
                speed = 2;
                dmg = 10;
                maxDelay = 1.3f;
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
        if(name == "S")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet1");
            dir.transform.position = transform.position;

            Bullet bu = dir.GetComponent<Bullet>();
            bu.dmg = dmg;

            Vector3 vec = playerT.position - transform.position;

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if(name == "L")
        {
            GameObject dirL = objMana.MakeObj("EnemyBullet2");
            GameObject dirR = objMana.MakeObj("EnemyBullet2");

            dirL.transform.position = transform.position + Vector3.left * 0.25f;
            dirR.transform.position = transform.position + Vector3.right * 0.25f;

            Bullet buL = dirL.GetComponent<Bullet>();
            buL.dmg = dmg;
            Bullet buR = dirR.GetComponent<Bullet>();
            buR.dmg = dmg;

            Vector3 vec = playerT.position - transform.position;

            Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();

            rigidL.AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            rigidR.AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        curDelay = 0;   
    }
}

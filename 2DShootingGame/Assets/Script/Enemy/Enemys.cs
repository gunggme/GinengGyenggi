using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] float hp;
    [SerializeField] public float dmg;
    [SerializeField] public float speed;
    [SerializeField] int score;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;


    ObjectManager objMana;
    SpriteRenderer spri;
    Transform playerT;


    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
        objMana = GameObject.Find("ObjectManager").gameObject.GetComponent<ObjectManager>();
        spri = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "EnemyS":
                hp = 5;
                dmg = 4;
                speed = 3f;
                score = 300;
                maxDelay = 1f;
                break;
            case "EnemySs":
                hp = 6;
                dmg = 6;
                speed = 4f;
                score = 350;
                maxDelay = 0.7f;
                break;
            case "EnemyM":
                hp = 10;
                dmg = 10;
                score = 400;
                speed = 10;
                break;
            case "EnemyL":
                hp = 15;
                dmg = 11;
                speed = 1;
                score = 500;
                maxDelay = 1f;
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
            GameObject dirS = objMana.MakeObj("EnemyBullet1");
            dirS.transform.position = transform.position;

            dirS.GetComponent<Bullet>().dmg = dmg;

            Vector3 dirST = playerT.position - transform.position;
                
            Rigidbody2D rigidS = dirS.GetComponent<Rigidbody2D>();

            rigidS.AddForce(dirST.normalized * 5, ForceMode2D.Impulse);
        }
        else if(name == "EnemySs")
        {
            GameObject dirSs = objMana.MakeObj("EnemyBullet2");
            dirSs.transform.position = transform.position;

            dirSs.GetComponent<Bullet>().dmg = dmg;

            Vector3 dirSsT = playerT.position - transform.position;

            Rigidbody2D rigidSs = dirSs.GetComponent<Rigidbody2D>();

            rigidSs.AddForce(dirSsT.normalized * 5, ForceMode2D.Impulse);
        }
        else if(name == "EnemyL")
        {
            GameObject dirR = objMana.MakeObj("EnemyBullet2");
            GameObject dirL = objMana.MakeObj("EnemyBullet2");

            dirR.transform.position = transform.position + Vector3.right * 0.25f;
            dirL.transform.position = transform.position + Vector3.left * 0.25f;

            dirR.GetComponent<Bullet>().dmg = dmg;
            dirL.GetComponent<Bullet>().dmg = dmg;

            Vector3 dirLT = playerT.position - transform.position;

            Rigidbody2D rigidL1 = dirR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL2 = dirL.GetComponent<Rigidbody2D>();

            rigidL1.AddForce(dirLT.normalized * 5, ForceMode2D.Impulse);
            rigidL2.AddForce(dirLT.normalized * 5, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 0)
        {
            gameObject.transform.rotation = Quaternion.identity;
            //gameManager에 스코어 추가하기
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnColor", 0.5f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet playerB = collision.gameObject.GetComponent<Bullet>();
            OnHit(playerB.dmg);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit(99999);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}

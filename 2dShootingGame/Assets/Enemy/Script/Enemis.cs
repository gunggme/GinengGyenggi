using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [Header("Enemy Stat")]
    [SerializeField] string name;
    public float speed;
    [SerializeField] float hp;
    [SerializeField] public float dmg;

    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    ObjectManager objMana;
    Transform playerT;
    SpriteRenderer spri;

    [SerializeField] string[] itemN;

    private void Awake()
    {
        itemN = new string[] { "Coin", "Power", "Fuer", "HP", "Coin", "Fuer", "Coin", "Fuer", "Fuer", "Fuer" };
        spri = GetComponent<SpriteRenderer>();
        playerT = GameObject.Find("Player").transform;
        objMana = GameObject.Find("ObjectManager").gameObject.GetComponent<ObjectManager>();
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "Meteo":
                hp = 5;
                speed = 4;
                dmg = 4;
                break;
            case "S":
                hp = 8;
                speed = 3;
                dmg = 2;
                maxDelay = 1;
                break;
            case "M":
                hp = 12;
                speed = 10;
                dmg = 8;
                break;
            case "L":
                hp = 15;
                speed = 1;
                dmg = 10;
                maxDelay = 1.5f;
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
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = objMana.MakeObj("EnemyBullet1");
            dir.transform.position = transform.position;

            Bullet bu = dir.GetComponent<Bullet>();
            bu.dmg = dmg;

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(name == "L")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dirL = objMana.MakeObj("EnemyBullet2");
            GameObject dirR = objMana.MakeObj("EnemyBullet2");

            Bullet bu = dirL.GetComponent<Bullet>();
            bu.dmg = dmg;
            Bullet bu2 = dirR.GetComponent<Bullet>();
            bu2.dmg = dmg;

            dirL.transform.position = transform.position + Vector3.left * 0.25f;
            dirR.transform.position = transform.position + Vector3.right * 0.25f;

            Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();

            rigidL.AddForce(vec.normalized * 5, ForceMode2D.Impulse);
            rigidR.AddForce(vec.normalized * 5, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    void OnHit(float lk)
    {
        hp -= lk;

        Effect();

        if(hp <= 0)
        {
            int ranItem = Random.Range(0, itemN.Length);
            transform.rotation = Quaternion.identity;
            GameObject dir = objMana.MakeObj(itemN[ranItem]);
            dir.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }

    void Effect()
    {
        spri.color = new Color(1, 1, 1, 0.6f);
        Invoke("ReturnColor", 0.4f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit(999999);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();

            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }

        
    }
}

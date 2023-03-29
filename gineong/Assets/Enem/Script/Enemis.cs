using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [SerializeField] string names;

    [Header("Stat")]
    [SerializeField] float hp;
    [SerializeField] float score;
    public float speed;

    [Header("Delay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] string[] itemNames;

    Transform playerT;



    private void OnEnable()
    {
        switch (names)
        {
            case "S":
                hp = 8;
                score = 300;
                speed = 3;
                maxDelay = 1.3f;
                break;
            case "M":
                hp = 12;
                score = 500;
                speed = 2;
                maxDelay = 1.2f;
                break;
            case "L":
                hp = 15;
                score = 600;
                speed = 1;
                maxDelay = 1f;
                break;
            case "Meteor1":
                hp = 6;
                score = 300;
                speed = 6;
                break;
            case "Meteor2":
                hp = 8;
                score = 500;
                speed = 10;
                break;
        }
    }

    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
        itemNames = new string[] { "HP", "Fuer", "MZ", "Null", "Power", "HP", "Fuer", "Null", "Fuer", "Coin" };
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

        if (names == "S")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }
        else if (names == "M")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }
        else if (names == "L")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position + Vector3.right * 0.25f;
            dir1.transform.position = transform.position + Vector3.left * 0.25f;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
            dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }
        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            int ranItem = Random.Range(0, itemNames.Length);
            if (itemNames[ranItem] != "Null")
            {
                GameObject dir = GameManager.instance.objMana.MakeObj(itemNames[ranItem]);
                dir.transform.position = transform.position;
            }
            GameManager.instance.score += score;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        Invoke("ReturnColor", 0.2f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
            OnHit(9999);
        if (collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}



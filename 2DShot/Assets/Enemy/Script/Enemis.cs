using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [SerializeField] string enemName;

    [Header("Enem Stat")]
    [SerializeField] float hp;
    public float speed;
    [SerializeField] float score;

    [Header("ShotDelay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] GameObject playerObj;
    [SerializeField] SpriteRenderer spri;

    string[] itemName;

    private void Awake()
    {
        itemName = new string[] { "Null", "Fuer", "Coin", "Fuer", "Null", "Fuer", "HP", "Null", "Fuer", "Fuer", "Null", "Fuer", "Power", "Fuer", "Null", "Fuer", "MZ" };
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        switch (enemName)
        {
            case "S":
                hp = 10;
                speed = 3;
                maxDelay = 1.4f;
                score = 300;
                break;
            case "M":
                hp = 15;
                speed = 2;
                maxDelay = 1.5f;
                score = 400;
                break;
            case "L":
                hp = 25;
                speed = 1;
                maxDelay = 1;
                score = 600;
                break;
            case "Meteo1":
                hp = 8;
                speed = 7;
                score = 300;
                break;
            case "Meteo2":
                hp = 11;
                speed = 8;
                score = 400;
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

        Vector3 vec = playerObj.transform.position - transform.position;

        if(enemName == "S")
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.position = transform.position;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }
        else if (enemName == "M")
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.position = transform.position;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }
        else if (enemName == "L")
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.position = transform.position + Vector3.right * 0.15f;
            dir2.transform.position = transform.position + Vector3.left * 0.15f;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 8, ForceMode2D.Impulse);
            rigid2.AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            int ranItemNum = Random.Range(0, itemName.Length);
            if (itemName[ranItemNum] != "Null")
            {
                GameObject dir = GameManager.instance.objMana.MakeObj(itemName[ranItemNum]);
                dir.transform.position = transform.position;
            }

            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.gameObject.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit(99999);
        }
    }
}

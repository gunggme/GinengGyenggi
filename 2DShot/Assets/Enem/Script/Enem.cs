using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enem : MonoBehaviour
{
    [SerializeField] string enemName;

    [Header("Stat")]
    public float speed;
    [SerializeField] float hp;
    [SerializeField] float score;

    [Header("Delay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField]
    string[] itemItems;

    Transform playerT;

    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
        itemItems = new string[] { null, "Fuer", "Fuer", "Coin", "Fuer", "Fuer", null, "Fuer", "Fuer", "HP", "Fuer", null, "HP", "Fuer", null, "HP", "Power", null, "Fuer", null, "MZ", null, null, null, "HP" };
    }

    private void OnEnable()
    {
        switch (enemName)
        {
            case "S":
                hp = 10;
                speed = 3;
                maxDelay = 1.3f;
                score = 400;
                break;
            case "M":
                hp = 15;
                speed = 3;
                score = 500;
                maxDelay = 1;
                break;
            case "L":
                hp = 20;
                speed = 1;
                score = 1000;
                maxDelay = 1;
                break;
            case "Meteor1":
                hp = 5;
                speed = 6;
                score = 300;
                break;
            case "Meteor2":
                hp = 10;
                speed = 7;
                score = 500;
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

        if(enemName == "S")
        {
            Vector3 vec = playerT.position - transform.position;

            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "M")
        {
            Vector3 vec = playerT.position - transform.position;

            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "L")
        {
            Vector3 vec = playerT.position - transform.position;

            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position + Vector3.right * 0.2f;
            dir2.transform.position = transform.position + Vector3.left * 0.2f;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            int ranItem = Random.Range(0, itemItems.Length);
            if (itemItems[ranItem] != null)
            {
                GameObject item = GameManager.instance.objMana.MakeObj(itemItems[ranItem]);
                item.transform.position = transform.position;
            }

            GameManager.instance.score += score;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.7f);
        Invoke("ReturnColor", 0.3f);
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
        {
            OnHit(9999);
        }
    }
}

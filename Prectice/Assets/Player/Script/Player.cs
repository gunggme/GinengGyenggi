using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] int hp;
    [SerializeField] float speed;
    [SerializeField] int power;

    [Header("Delay")]
    [SerializeField]
    Transform[] firePosition;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Sprite[] sprite;

    bool isHit;

    private void Update()
    {
        Move();
        Shot();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        switch (h)
        {
            case -1:
                spri.sprite = sprite[0];
                break;
            case 0:
                spri.sprite = sprite[1];
                break;
            case 1:
                spri.sprite = sprite[2];
                break;
        }

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x < 0) pos.x = 0;
        if(pos.x > 1) pos.x = 1;
        if(pos.y < 0) pos.y = 0;
        if(pos.y > 1) pos.y = 1;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    } 

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            switch (power)
            {
                case 1:
                    GameObject dirC = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirC.transform.position = firePosition[0].transform.position;

                    dirC.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    break;
                case 2:
                    GameObject dirL = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dirR = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirL.transform.position = firePosition[1].transform.position;
                    dirR.transform.position = firePosition[2].transform.position;

                    dirL.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    dirR.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    break;
                case 3:
                    GameObject dirC3 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    dirC3.transform.position = firePosition[0].transform.position;

                    dirC3.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    break;
                case 4:
                    GameObject dirL4 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dirC4 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    GameObject dirR4 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirL4.transform.position = firePosition[1].transform.position;
                    dirC4.transform.position = firePosition[1].transform.position;
                    dirR4.transform.position = firePosition[2].transform.position;

                    dirL4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    dirC4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    dirR4.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 14, ForceMode2D.Impulse);
                    break;
            }
            curDelay = 0;
        }
    }

    void OnHit()
    {
        hp--;

        OnHitEffect();
        Invoke("ReturnColor", 1);
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.6f);
        isHit = true;
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit)
        {
            if(collision.gameObject.CompareTag("EnemBullet"))
            {
                OnHit();
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.CompareTag("Enem"))
            {
                OnHit();
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.itemName)
            {
                case "Coin":
                    break;
                case "Power":
                    break;
                case "HP":
                    break;
                case "Fuer":
                    break;
                case "MZ":
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}

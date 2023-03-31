using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stat")]
    public int hp;
    public int power;
    public float fuel;
    [SerializeField] float speed;

    [Header("Shot")]
    [SerializeField]
    Transform[] firePosition;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Color[] colors;
    [SerializeField] bool isHit;
    [SerializeField] bool isDown;

    private void Start()
    {
        InvokeRepeating("FuerDown", 1, 1);
    }

    void Update()
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
                spri.sprite = sprites[0]; 
                break;
            case 0:
                spri.sprite = sprites[1];
                break;
            case 1:
                spri.sprite = sprites[2];
                break;
        }

        transform.position += new Vector3(h, v) * speed * Time.deltaTime;

        Vector3 po = Camera.main.WorldToViewportPoint(transform.position);
        if(po.x < 0) po.x = 0;
        if(po.x > 1) po.x = 1;
        if(po.y < 0) po.y = 0;
        if(po.y > 1) po.y = 1;
        transform.position = Camera.main.ViewportToWorldPoint(po);
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
                    GameObject dir = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dir.transform.position = firePosition[0].transform.position;

                    dir.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 2:
                    GameObject dirL2 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dirR2 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dirL2.transform.position = firePosition[1].transform.position;
                    dirR2.transform.position = firePosition[2].transform.position;

                    dirL2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dirR2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 3:
                    GameObject dirC2 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    dirC2.transform.position = firePosition[0].transform.position;

                    dirC2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
                case 4:
                    GameObject dir1 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    GameObject dir2 = GameManager.instance.objMana.MakeObj("PlayerBullet2");
                    GameObject dir3 = GameManager.instance.objMana.MakeObj("PlayerBullet1");
                    dir1.transform.position = firePosition[0].transform.position;
                    dir2.transform.position = firePosition[1].transform.position;
                    dir3.transform.position = firePosition[2].transform.position;

                    dir1.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dir2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    dir3.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 13, ForceMode2D.Impulse);
                    break;
            }
            curDelay = 0;
        }
    }

    void FuerDown()
    {
        fuel -= 2;
    }

    void OnHit()
    {
        hp--;
        OnHitEffect();
        Invoke("ReturnColor", 1);
        
    }

    void OnHitEffect()
    {
        spri.color = colors[1];
        gameObject.layer = 8;
        isHit = true;
    }

    void ReturnColor()
    {
        spri.color = colors[0];
        isHit = false;
        gameObject.layer = 7;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("EnemBullet"))
            {
                OnHit();
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.CompareTag("Enem"))
                OnHit();
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}

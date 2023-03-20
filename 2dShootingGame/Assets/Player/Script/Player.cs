using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] float speed;
    [SerializeField] int power;
    [SerializeField] public int hp;

    [Header("PlayerShotDelay")]
    [SerializeField] Transform[] firePositions;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("PlayerSprite")]
    [SerializeField] Sprite[] playerSprite;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;

    bool isHit = false;

    private void Update()
    {
        Move();
        Shot();
    }

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }
        if (!Input.GetKey(KeyCode.Z))
            return;

        switch (power)
        {
            case 1:
                GameObject dirC = GameManager.Instance.objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = firePositions[0].position;

                dirC.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject dirL2 = GameManager.Instance.objMana.MakeObj("PlayerBullet1");
                GameObject dirR2 = GameManager.Instance.objMana.MakeObj("PlayerBullet1");
                dirL2.transform.position = firePositions[2].position;
                dirR2.transform.position = firePositions[1].position;
                Rigidbody2D rigidL2 = dirL2.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR2 = dirR2.GetComponent<Rigidbody2D>();
                
                rigidL2.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidR2.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject dirC3 = GameManager.Instance.objMana.MakeObj("PlayerBullet2");
                GameObject dirL3 = GameManager.Instance.objMana.MakeObj("PlayerBullet1");
                GameObject dirR3 = GameManager.Instance.objMana.MakeObj("PlayerBullet1");
                dirC3.transform.position = firePositions[0].position;
                dirL3.transform.position = firePositions[2].position;
                dirR3.transform.position = firePositions[1].position;

                Rigidbody2D rigidC3 = dirC3.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL3 = dirR3.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR3 = dirL3.GetComponent<Rigidbody2D>();

                rigidC3.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidR3.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidL3.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirC4 = GameManager.Instance.objMana.MakeObj("PlayerBullet2");
                GameObject dirL4 = GameManager.Instance.objMana.MakeObj("PlayerBullet2");
                GameObject dirR4 = GameManager.Instance.objMana.MakeObj("PlayerBullet2");
                dirC4.transform.position = firePositions[0].position;
                dirL4.transform.position = firePositions[2].position;
                dirR4.transform.position = firePositions[1].position;

                Rigidbody2D rigidC4 = dirC4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL4 = dirR4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR4 = dirL4.GetComponent<Rigidbody2D>();

                rigidC4.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidR4.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidL4.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }

        curDelay = 0;
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(h , Input.GetAxisRaw("Vertical"), 0) * speed * Time.deltaTime;

        if(h == 0)
        {
            spri.sprite = playerSprite[1];
        }
        else if(h == -1)
        {
            spri.sprite = playerSprite[0];
        }
        else if(h == 1)
        {
            spri.sprite = playerSprite[2];
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x < 0) pos.x = 0;
        if(pos.x > 1)   pos.x = 1;
        if(pos.y < 0) pos.y = 0;
        if(pos.y > 1) pos.y = 1;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnHit()
    {
        hp--;

        OnHitEffect();
        Invoke("ReturnEffect", 0.6f);
        GameManager.Instance.PlayaerHP();

        if(hp < 1)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        isHit = true;
        gameObject.layer = 8;
    }

    void ReturnEffect()
    {
        spri.color = new Color(1, 1, 1, 1);
        isHit = false;
        gameObject.layer = 7;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                OnHit();
                collision.gameObject.SetActive(false);
            }
        }
    }
}

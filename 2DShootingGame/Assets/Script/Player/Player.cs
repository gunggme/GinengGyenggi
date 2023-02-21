using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� ���� ����")]
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] int power;

    [Header("�÷��̾� �� ������")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("�Ŵ��� ����")]
    [SerializeField] ObjectManager objMana;

    [SerializeField] bool isHit;

    Animator ani;
    SpriteRenderer spri;

    private void Awake()
    {
        maxDelay = 0.3f;
        //������Ʈ �ҷ�����
        ani = GetComponent<Animator>();
        spri = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Shot();
    }

    //�����̴� ��ǰ� �����̴� �Լ�
    void Move()
    {
        //x��
        float h = Input.GetAxisRaw("Horizontal");
        //Y��
        float v = Input.GetAxisRaw("Vertical");

        //h���� int������ �ٲ۵� isMove �Ķ���Ͱ� ����
        ani.SetInteger("isMove", (int)h);

        //���� ���� ��ư������ ��ġ ����
        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        //ī�޶� �������� ī�޶� ������ �������� �����
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if(pos.x < 0) pos.x = 0;
        if(pos.x > 1) pos.x = 1;
        if(pos.y < 0) pos.y = 0;
        if(pos.y > 1) pos.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //�Ѿ� �߻�
    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if (!Input.GetButton("Fire1"))
            return;
        switch (power)
        {
            case 1:
                //playerBullet1�̶�� ������Ʈ�� ������ dirC�� �ʱ�ȭ
                GameObject dirC = objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = transform.position;

                //dirC�� �ִ� Rigidbody2D�� ������ �ʱ�ȭ
                Rigidbody2D rigidC = dirC.GetComponent<Rigidbody2D>();

                //AddForce�� ���־� ���� �÷�������
                rigidC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject dirR = objMana.MakeObj("PlayerBullet1");
                GameObject dirL = objMana.MakeObj("PlayerBullet1");

                dirR.transform.position = transform.position + Vector3.right * 0.25f;
                dirL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject dirCCC = objMana.MakeObj("PlayerBullet2");

                dirCCC.transform.position = transform.position;

                Rigidbody2D rigidCCC = dirCCC.GetComponent<Rigidbody2D>();

                rigidCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirRRRR = objMana.MakeObj("PlayerBullet1");
                GameObject dirCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirLLLL = objMana.MakeObj("PlayerBullet1");

                dirRRRR.transform.position = transform.position + Vector3.right * 0.3f;
                dirCCCC.transform.position = transform.position;
                dirLLLL.transform.position = transform.position + Vector3.left * 0.3f;

                Rigidbody2D rigidRRRR = dirRRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCC = dirCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLLL = dirLLLL.GetComponent<Rigidbody2D>();

                rigidRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);

                break;
            case 5:
                GameObject dirRRRRR = objMana.MakeObj("PlayerBullet2");
                GameObject dirCCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirLLLLL = objMana.MakeObj("PlayerBullet2");

                dirRRRRR.transform.position = transform.position + Vector3.right * 0.3f;
                dirCCCCC.transform.position = transform.position;
                dirLLLLL.transform.position = transform.position + Vector3.left * 0.3f;

                Rigidbody2D rigidRRRRR = dirRRRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCCC = dirCCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLLLL = dirLLLLL.GetComponent<Rigidbody2D>();

                rigidRRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }

        curDelay= 0;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();
        Invoke("ReturnColor", 1.5f);

        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        gameObject.layer = 9;
        isHit = true;
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
        gameObject.layer = 8;
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnHit(bullet.dmg);
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemys enemy = collision.gameObject.GetComponent<Enemys>();
                OnHit(enemy.dmg / 2);
            }
        }
    }
}

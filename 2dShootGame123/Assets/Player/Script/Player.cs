using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] float power;

    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [SerializeField] float curSkill1Delay;
    [SerializeField] float curSkill2Delay;
    [SerializeField] float skill1Delay;
    [SerializeField] float skill2Delay;

    [Header("ETC")]
    [SerializeField] Animator playerA;
    [SerializeField] ObjManager objMana;

    private void Awake()
    {
        hp = 30;
        maxDelay = 0.3f;
    }

    private void Update()
    {
        Move();
        Shot();
        Skill1();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        playerA.SetInteger("isMove", (int)h);

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

        if (!Input.GetKey(KeyCode.Z))
            return;

        switch (power)
        {
            case 1:
                GameObject dirC = objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = transform.position;

                Rigidbody2D rigid = dirC.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject dirRR = objMana.MakeObj("PlayerBullet1");
                GameObject dirLL = objMana.MakeObj("PlayerBullet1");
                dirRR.transform.position = transform.position + Vector3.right * 0.25f; 
                dirLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRR = dirRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = dirLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject dirCCC = objMana.MakeObj("PlayerBullet1");
                dirCCC.transform.position = transform.position;

                Rigidbody2D rigidCCC = dirCCC.GetComponent<Rigidbody2D>();
                rigidCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirRRRR = objMana.MakeObj("PlayerBullet1");
                GameObject dirCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirLLLL = objMana.MakeObj("PlayerBullet1");
                dirRRRR.transform.position = transform.position + Vector3.right * 0.25f;
                dirCCCC.transform.position = transform.position;
                dirLLLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRRRR = dirRRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCC = dirCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLLL = dirLLLL.GetComponent<Rigidbody2D>();
                rigidRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }

        curDelay = 0;
    }

    void Skill1()
    {
        if(curSkill1Delay < skill1Delay)
        {
            curSkill1Delay += Time.deltaTime;
            return;
        }

        hp += 10;
        curSkill1Delay = 0;
    }
}

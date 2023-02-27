using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] int power;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("Managers")]
    [SerializeField] ObjectManager objMana;

    [Header("Etc")]
    [SerializeField] Animator ani;

    private void Awake()
    {
        hp = 30;
    }

    private void Update()
    {
        Move();
        Shot();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        ani.SetInteger("isMove", (int)h);

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0) pos.x = 0;
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
        if (!Input.GetButton("Fire1"))
            return;

        switch (power)
        {
            case 1:
                GameObject dirC = objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = transform.position;

                Rigidbody2D rigidC = dirC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
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
                GameObject dirCCC = objMana.MakeObj("PlayerBullet2");
                dirCCC.transform.position = transform.position;

                Rigidbody2D rigidCCC = dirCCC.GetComponent<Rigidbody2D>();
                rigidCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirLLLL = objMana.MakeObj("PlayerBullet1");
                GameObject dirCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirRRRR = objMana.MakeObj("PlayerBullet1");

                dirLLLL.transform.position = transform.position + Vector3.left * 0.25f;
                dirCCCC.transform.position = transform.position;
                dirRRRR.transform.position = transform.position + Vector3.right * 0.25f;

                Rigidbody2D rigidLLLL = dirLLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCC = dirCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRRR = dirRRRR.GetComponent<Rigidbody2D>();

                rigidLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject dirLLLLL = objMana.MakeObj("PlayerBullet2");
                GameObject dirCCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirRRRRR = objMana.MakeObj("PlayerBullet2");

                dirLLLLL.transform.position = transform.position + Vector3.left * 0.3f;
                dirCCCCC.transform.position = transform.position;
                dirRRRRR.transform.position = transform.position + Vector3.right * 0.3f;

                Rigidbody2D rigidLLLLL = dirLLLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCCC = dirCCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRRRR = dirRRRRR.GetComponent<Rigidbody2D>();

                rigidLLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }
        curDelay = 0;
    }
}

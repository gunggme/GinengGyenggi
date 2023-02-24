using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float speed;
    [SerializeField] public float hp;
    [SerializeField] int power;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("BoomItem Effect")]
    [SerializeField] GameObject boomEffect;

    [Header("OnHit Check")]
    [SerializeField] bool isHit;

    [Header("Managers")]
    [SerializeField] ObjectManager objMana;
    [SerializeField] GameManager gameMana;

    Animator ani;
    SpriteRenderer spri;

    bool isMz;
    Coroutine coru; 

    private void Awake()
    {
        spri = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shot();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        ani.SetInteger("isMove", (int)h);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0) pos.x = 0;
        if (pos.x > 1) pos.x = 1;
        if (pos.y < 0) pos.y = 0;
        if (pos.y > 1) pos.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Shot()
    {
        if (curDelay < maxDelay)
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
                rigidLLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }
        curDelay = 0;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();
        Invoke("ReturnColor", 1.5f);

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        isHit = true;
        gameObject.layer = 10;
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
        isHit = false;
        gameObject.layer = 9;
    }

    void ReturnColor2()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
    }

    IEnumerator MZ()
    {
        isMz = true;
        OnHitEffect();
        yield return new WaitForSeconds(2.4f);
        ReturnColor2();
        yield return new WaitForSeconds(0.1f);
        OnHitEffect();
        yield return new WaitForSeconds(0.1f);
        ReturnColor2();
        yield return new WaitForSeconds(0.1f);
        OnHitEffect();
        yield return new WaitForSeconds(0.1f);
        ReturnColor2();
        yield return new WaitForSeconds(0.1f);
        OnHitEffect();
        yield return new WaitForSeconds(0.1f);
        ReturnColor();
        isMz = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isHit)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemys enem = collider.gameObject.GetComponent<Enemys>();
                OnHit(enem.dmg / 2);
            }
            if (collider.gameObject.CompareTag("EnemyBullet") || collider.gameObject.CompareTag("BossBullet"))
            {
                Bullets b = collider.gameObject.GetComponent<Bullets>();
                OnHit(b.dmg);
            }

            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.CompareTag("Item"))
        {
            Item item = collider.gameObject.GetComponent<Item>();

            switch (item.name)
            {
                case "Coin":
                    //점수 추가하기
                    gameMana.score += 300;
                    break;
                case "Power":
                    //power + 1하는데 만약 5까지 찬다면 점수로 변환
                    if(power >= 5)
                    {
                        gameMana.score += 200;
                    }
                    else
                    {
                        power++;
                    }
                    break;
                case "Hp":
                    //hp + 5하는데 만약 30까지 다 찬다면 점수로 변환
                    if(hp >= 30)
                    {
                        gameMana.score += 200;
                    }
                    else
                    {
                        hp += 5;
                    }
                    break;
                case "MZItem":
                    //무적 아이템 3초동안 지속, 만약 3초가 지나가기 전에 먹으면 시간 초기화
                    if (!isMz)
                    {
                        coru = StartCoroutine("MZ");
                    }
                    else
                    {
                        StopCoroutine(coru);
                        coru = StartCoroutine("MZ");
                    }
                    break;
                case "SickDown":
                    //고통 게이지를 줄여주는 아이템 단 0이하로 안내려간다.
                    gameMana.curSick -= 10;
                    break;
                case "Boom":
                    //화면 전체에 있는 적, 총알들 삭제 하지만 보스에겐 데미지 안들어감
                    boomEffect.gameObject.SetActive(true);
                    break;
            }

            collider.gameObject.SetActive(false);
        }
    }
}

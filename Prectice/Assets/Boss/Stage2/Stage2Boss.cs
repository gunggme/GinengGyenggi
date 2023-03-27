using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss : MonoBehaviour
{
    [Header("Stat")]
    public float hp;

    [Header("Skill")]
    [SerializeField] int skillIndex;
    [SerializeField] int maxSkillStack;
    [SerializeField] int curSkillStack;

    [Header("ETC")]
    [SerializeField]
    Transform[] firePosition;
    [SerializeField] Transform playerT;
    [SerializeField] SpriteRenderer spri;

    [SerializeField] bool isMove;
    [SerializeField] int a = 1;

    private void OnEnable()
    {
        isMove = true;
        a = 1;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if (transform.position.y < 2)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }
    void Think()
    {
        curSkillStack = 0;
        maxSkillStack = Random.Range(6, 8);
        skillIndex = Random.Range(0, 5);

        switch (skillIndex)
        {
            case 0:
                Invoke("PlayerShotGun", 1);
                break;
            case 1:
                Invoke("CsicleShot", 1);
                break;
            case 2:
                Invoke("ArkShot", 1);
                break;
            case 3:
                Invoke("SpawnOn", 1);
                break;
            case 4:
                Invoke("ArkShot2", 1);
                break;
            case 5:
                Think();
                break;

        }
    }
    void PlayerShotGun()
    {
        Vector3 vec = playerT.position - transform.position;

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet2");

        dir1.transform.position = firePosition[0].transform.position + Vector3.right * 0.4f;
        dir2.transform.position = firePosition[0].transform.position + Vector3.right * 0.2f;
        dir3.transform.position = firePosition[0].transform.position + Vector3.left * 0.2f;
        dir4.transform.position = firePosition[0].transform.position + Vector3.left * 0.4f;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShotGun", 0.3f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void CsicleShot()
    {
        int round = 30;
        for (int i = 0; i < round; i++)
        {
            Vector3 vec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / round), Mathf.Sin(Mathf.PI * 2 * i / round));
            GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet3");

            dir1.transform.position = firePosition[0].position;
            dir1.transform.rotation = Quaternion.identity;

            dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("CsicleShot", 0.6f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
        switch (a)
        {
            case 1:
                a = 2;
                break;
            case 2:
                a = 1;
                break;
        }
        dir.transform.position = firePosition[a].position;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < 20)
        {
            Invoke("ArkShot", 0.1f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot2()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
        switch (a)
        {
            case 1:
                a = 2;
                break;
            case 2:
                a = 1;
                break;
        }
        dir.transform.position = firePosition[a].position;  

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet2");

        dir1.transform.position = firePosition[0].transform.position + Vector3.right * 0.4f;
        dir2.transform.position = firePosition[0].transform.position + Vector3.right * 0.2f;
        dir3.transform.position = firePosition[0].transform.position + Vector3.left * 0.2f;
        dir4.transform.position = firePosition[0].transform.position + Vector3.left * 0.4f;

        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        curSkillStack++;
        if (curSkillStack < 20)
        {
            Invoke("ArkShot2", 0.1f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void SpawnOn()
    {
        GameManager.instance.spawnMana.isSpawn = true;
        Invoke("SpawnOff", 15f);
        Think();
    }

    void SpawnOff()
    {
        GameManager.instance.spawnMana.isSpawn = false;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if (hp < 1)
        {
            CancelInvoke();
            GameManager.instance.score += 1000;
            gameObject.SetActive(false);
        }
    }


    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        Invoke("ReturnColor", 0.25f);
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
    }
}

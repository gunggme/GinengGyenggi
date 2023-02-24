using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Stat")]
    [SerializeField] float hp;
    [SerializeField] int score;

    bool onMove;

    [Header("Skill Index & Skill Stack")]
    [SerializeField] int curSkillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillstack;

    [Header("Manager")]
    [SerializeField] ObjectManager objMana;
    [SerializeField] SpawnManager spawnMana;
    [SerializeField] GameManager gameMana;

    [Header("Player Transform")]
    [SerializeField] Transform playerT;

    SpriteRenderer spri;

    private void Awake()
    {
        spri = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        //활성화시 이제 다른 오브젝트 생성 안되게 제어
        spawnMana.spawnStop = true;
        //활성화 되었을때 움직일수 있게 설정
        onMove = true;
        //활성화 되었을때 위치를 y 6으로 이동
        transform.position = new Vector2(0, 6);

        //활성화 되었을때 기본 스탯 설정
        hp = 1000;
        score = 10000;

    }

    private void Update()
    {
        //onMove가 true상태일때
        if (onMove)
        {
            //object의 position의 y값을 내림
            transform.position += Vector3.down * 2 * Time.deltaTime;

            //만약 y값이 3이 되거나 작을때
            if (transform.position.y <= 3)
            {
                //onMove의 값을 false로 변경
                onMove = false;
                //1초뒤 스킬 사용 준비
                Invoke("Think", 1f);
            }
        }
    }

    void Think()
    {
        curSkillIndex = Random.Range(0, 3);
        curSkillStack = 0;
        maxSkillstack = Random.Range(3, 6);

        switch (curSkillIndex)
        {
            case 0:
                Invoke("ShotGun", 0.5f);
                break;
            case 1:
                Invoke("PlayerShot", 0.5f);
                break;
            case 2:
                Invoke("ArkShot", 0.5f);
                break;
            case 3:
                Think();
                break;
        }
    }

    void ShotGun()
    {
        //전방으로 4발 발사
        GameObject dirL = objMana.MakeObj("BossBullet2");
        GameObject dirC1 = objMana.MakeObj("BossBullet1");
        GameObject dirC2 = objMana.MakeObj("BossBullet1");
        GameObject dirR = objMana.MakeObj("BossBullet2");

        dirL.transform.position = transform.position + Vector3.left * 0.35f;
        dirC1.transform.position = transform.position + Vector3.left * 0.15f;
        dirC2.transform.position = transform.position + Vector3.right * 0.15f;
        dirR.transform.position = transform.position + Vector3.right * 0.35f;

        Rigidbody2D rigid1 = dirL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dirC1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dirC2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dirR.GetComponent<Rigidbody2D>();

        rigid1.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigid2.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigid3.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigid4.AddForce(Vector3.down * 7, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillstack)
        {
            Invoke("ShotGun", 1f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void PlayerShot()
    {
        //플레이어를 향해 4발 발사
        Vector3 dir = playerT.position - transform.position;

        GameObject dirL = objMana.MakeObj("BossBullet2");
        GameObject dirC1 = objMana.MakeObj("BossBullet1");
        GameObject dirC2 = objMana.MakeObj("BossBullet1");
        GameObject dirR = objMana.MakeObj("BossBullet2");

        dirL.transform.position = transform.position + Vector3.left * 0.35f;
        dirC1.transform.position = transform.position + Vector3.left * 0.15f;
        dirC2.transform.position = transform.position + Vector3.right * 0.15f;
        dirR.transform.position = transform.position + Vector3.right * 0.35f;

        Rigidbody2D rigid1 = dirL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dirC1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dirC2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dirR.GetComponent<Rigidbody2D>();

        rigid1.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigid2.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigid3.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigid4.AddForce(dir.normalized * 7, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillstack)
        {
            Invoke("PlayerShot", 1f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void ArkShot()
    {
        //플레이어 위치를 기준으로 랜덤 위치 발사
        //플레이어 위치 찾기
        Vector3 dir = playerT.position - transform.position;
        Vector3 dirRan = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0, 2));
        dir -= dirRan;

        GameObject obj = objMana.MakeObj("BossBullet1");
        obj.transform.position = transform.position;

        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();

        rigid.AddForce(dir.normalized * 8, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < 20)
        {
            Invoke("ArkShot", 0.5f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void ArkShotUp()
    {
        //플레이어 위치를 기준으로 랜덤 위치 발사
        //플레이어 위치 찾기
        Vector3 dir = playerT.position - transform.position;
        Vector3 dirRan = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0, 2));
        dir -= dirRan;

        GameObject obj = objMana.MakeObj("BossBullet2");
        obj.transform.position = transform.position;

        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();

        rigid.AddForce(dir.normalized * 8, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < 20)
        {
            Invoke("ArkShotUp", 0.5f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if (hp < 0)
        {
            gameMana.StageEnd();
            CancelInvoke();
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
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
            Bullets bullet = collision.GetComponent<Bullets>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }

}

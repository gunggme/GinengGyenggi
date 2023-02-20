using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�Ѿ��� ������
    public float dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("BossBullet"))
                return;
            gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}

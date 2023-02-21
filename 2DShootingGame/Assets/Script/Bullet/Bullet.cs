using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알의 데미지
    public float dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("BossBullet"))
                return;
            gameObject.SetActive(false);
        }

        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("BossBullet") || collision.gameObject.CompareTag("Item"))
                return;
            gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}

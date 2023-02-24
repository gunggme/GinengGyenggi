using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("BossBullet") || collision.gameObject.CompareTag("Item"))
            return;

 

        

        if (collision.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}

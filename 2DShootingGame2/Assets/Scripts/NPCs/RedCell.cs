using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : MonoBehaviour
{
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}

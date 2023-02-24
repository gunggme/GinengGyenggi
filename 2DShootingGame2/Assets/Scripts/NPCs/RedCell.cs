using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : MonoBehaviour
{
    public float speed;

    GameManager gameMana;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameMana.curSick += 10;
            gameObject.SetActive(false);
        }
    }
}

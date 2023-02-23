using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : MonoBehaviour
{
    public float speed;

    GameManager gameMana;

    private void Awake()
    {
        gameMana = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
            return;
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameMana.curSick += 5;
            gameObject.SetActive(false);
        }
        if (collision.attachedRigidbody.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}

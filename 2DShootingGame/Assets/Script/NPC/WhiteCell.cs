using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : MonoBehaviour
{
    public float speed;

    ObjectManager objMana;
    string[] items;

    private void Awake()
    {
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        items = new string[] { "Coin", "HP", "Coin", "Power", "MZItem", "SickDown", "Boom", "Coin", "HP", "Coin" };
    }

    void DropItem()
    {
        int rI = Random.Range(0, items.Length - 1);

        GameObject dir = objMana.MakeObj(items[rI]);
        dir.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            DropItem();
            gameObject.SetActive(false);
        }

        if (collision.attachedRigidbody.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}

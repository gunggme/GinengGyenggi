using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WhiteCell : MonoBehaviour
{
    string[] items = { "Coin", "Coin", "Power", "Hp", "MZItem", "SickDown", "Coin"};

    ObjectManager objMana;

    public float speed;

    private void Awake()
    {
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    void DropItem()
    {
        int randomItem = Random.Range(0, items.Length - 1);

        GameObject dir = objMana.MakeObj(items[randomItem]);
        dir.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.identity;
            DropItem();
            gameObject.SetActive(false);
        }

        
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}

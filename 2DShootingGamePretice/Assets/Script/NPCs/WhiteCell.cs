using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : MonoBehaviour
{
    string[] itemNames;

    ObjectManager objMana;

    private void Awake()
    {
        itemNames = new string[] { "Coin", "Power", "HP", "SickDown" };
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    void DropItem()
    {
        int itemIndex = Random.Range(0, itemNames.Length - 1);
        GameObject item = objMana.MakeObj(itemNames[itemIndex]);
        item.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            DropItem();
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
            
    }
}

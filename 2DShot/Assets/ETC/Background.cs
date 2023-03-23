using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float range;

    [SerializeField] SpriteRenderer spri;
    [SerializeField] Sprite[] sprite;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y <= -range)
        {
            transform.position = target.position + Vector3.up * range;
        }

        //�������� �ѿ� ���� �ٸ� ��������Ʈ�� �ٲ�� �����
    }
}

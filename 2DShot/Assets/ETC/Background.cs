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

        //스테이지 넘에 따라 다른 스프라이트로 바뀌게 만들기
    }
}

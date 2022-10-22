using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Transform target;
    public float speed;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = FindObjectOfType<Playerable>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        rigid.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Playerable>())
            Debug.Log("Player Attack");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Damage;

    BoxCollider2D collider;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();

        if (hitEnemy != null)
            hitEnemy.GetDamage(Damage);
    }

    
}

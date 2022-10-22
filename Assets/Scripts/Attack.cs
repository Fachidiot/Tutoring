using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float damage = 20f;
    float addedDamage;

    public void AddDamage(float combo)
    {
        addedDamage = damage * combo;
    }

    public void ResetDamage()
    {
        addedDamage = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();

        if (hitEnemy != null)
        {
            hitEnemy.GetComponent<Rigidbody2D>().AddForce(transform.position - hitEnemy.transform.position);
            hitEnemy.GetDamage(addedDamage == 0 ? damage : addedDamage);
        }
    }    
}

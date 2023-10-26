using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float health =100.0f;

    void GetDamaged(float damage)
    {
        print("Damaged, current health: "+health);
        health -= damage;
        if(health <= 0.0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float health =100.0f;

    void GetDamaged(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            Die();
        }
        print("Damaged, current health: "+health);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

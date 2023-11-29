using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float damage = 30.0f;
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.SendMessage("GetDamaged",damage);
            Destroy(gameObject);
        }
    }
    public void SetDamage(float currentDamage){damage = currentDamage;}
}

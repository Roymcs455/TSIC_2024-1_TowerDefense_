using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.SendMessage("GetDamaged",30);
            Destroy(gameObject);
        }
    }
}

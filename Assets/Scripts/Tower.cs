using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform objective;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Colisión");
        if (collision.tag=="Enemy")
        {
            Debug.Log("Entra enemigo");
            collision.SendMessage("Die");
            //objective=collision.transform;
        }        
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        
    }

}

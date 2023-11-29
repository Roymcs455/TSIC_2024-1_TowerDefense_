using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
    public float size = 1.0f;
    public float damage =  1.0f;
    public float explosionArea = 5.0f;
    public float timeToLive = 3.0f;
    private Vector3 direction;


    void Start(){
        Destroy(gameObject,timeToLive);
        gameObject.transform.localScale = new Vector3(size,size,size);
    }
    void Update()
    {
        if (target == null)
        {
            //Destroy(gameObject);
        }
        else
        {
            direction = (target.position - transform.position).normalized;
        }
        Vector3 velocity = direction * speed;
        transform.Translate(velocity * Time.deltaTime);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag=="Enemy")
        {
            //collision.SendMessage("GetDamaged",damage);
            Explode();
            Destroy(gameObject);
        }
        if (collision.tag=="Floor")
        {
            Explode();
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// función que crea  
    /// </summary>
    void Explode( )
    {
        GameObject explodeArea =  new GameObject("ExplodeZone");
        explodeArea.AddComponent<Explosion>().SetDamage(damage);
        explodeArea.transform.position = transform.position;
        SphereCollider collider = explodeArea.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = explosionArea;

    }
    public void SetDAmage(float currentDamage){damage = currentDamage;}
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void SetDamage (float currentDamage){damage = currentDamage;}
}

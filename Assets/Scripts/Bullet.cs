using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
    public float size = 1.0f;
    public float damage =  1.0f;
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
            collision.SendMessage("Die");
            Destroy(gameObject);
        }
        if (collision.tag=="Floor")
        {
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

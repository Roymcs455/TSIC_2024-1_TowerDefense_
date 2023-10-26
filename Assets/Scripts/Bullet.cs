using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 velocity = direction * speed;
        transform.Translate(velocity * Time.deltaTime);
    }
    void OnTriggerEnter(Collider collision)
    {

    }
}

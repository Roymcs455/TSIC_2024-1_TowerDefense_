using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> enemiesAtRange;
    public GameObject objective;
    public Transform muzzle;
    public Bullet projectile;
    public float bulletSize =0.5f;
    public float fireRate = 01.5f;
    private float nextFireTime =0.0f;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Colisión");
        if (collision.tag=="Enemy")
        {
            Debug.Log("Entra enemigo");
            //collision.SendMessage("Die");
            enemiesAtRange.Add(collision.gameObject);
            //objective=collision.transform;
        }        
    }
    void OnTriggerExit(Collider collision)
    {
        if(enemiesAtRange.Contains(collision.gameObject))
        {
            enemiesAtRange.Remove(collision.gameObject);
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
    public Transform SelectNewTarget()
    {
        float minimalDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemiesAtRange)
        {
            if(enemy == null)
                enemiesAtRange.Remove(enemy);
            else
            {
                float distance = Vector3.Distance(transform.position,enemy.GetComponent<Transform>().position);
                if(distance<minimalDistance)
                {
                    minimalDistance = distance;
                    objective = enemy;
                }
            }
        }
        
        return objective.transform;
    }
    void Update()
    {
        
        if (enemiesAtRange.Count !=0)
        {
            if(objective == null)
                SelectNewTarget();
            if(Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime =Time.time + 1f/fireRate;
            }
        }
        muzzle.LookAt(objective.transform);
    }

    private void Shoot()
    {
        Bullet newProjectile = Instantiate(projectile, muzzle.transform.position, Quaternion.identity);
        newProjectile.SetTarget(objective.transform);
        newProjectile.size = bulletSize;
    }
}

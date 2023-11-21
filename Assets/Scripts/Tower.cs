using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    enum towerType 
    {
        Archer,
        Bomb,
        Mage
    }
    public List<GameObject> enemiesAtRange;
    public GameObject objective;
    public Transform muzzle;
    public GameObject arrow;
    public GameObject bomb;
    
    public float bulletSize =0.5f;
    public float fireRate = 01.5f;
    [SerializeField] towerType type = towerType.Archer;
    private float nextFireTime =0.0f;
    private SphereCollider sphereCollider;
    
    
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 10;
        
    }
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
        enemiesAtRange.Remove(collision.gameObject);
        
        
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
            if(Math.Abs((objective.transform.position - transform.position).magnitude) > sphereCollider.radius )
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
        switch (type)
        {
            case towerType.Archer:
                GameObject arrowProjectile = Instantiate(arrow, muzzle.transform.position, Quaternion.identity);
                Bullet newBullet = arrowProjectile.GetComponent<Bullet>();
                newBullet.SetTarget(objective.transform);
                newBullet.size = bulletSize;
            break;
            case towerType.Bomb:
                GameObject bombProjectile = Instantiate(bomb, muzzle.transform.position, Quaternion.identity);
                Bomb newBomb = bombProjectile.GetComponent<Bomb>();
                newBomb.SetTarget(objective.transform);
                newBomb.size = bulletSize;
            break;
            case towerType.Mage:
            break;
            default:
            break;
        }
        
    }
}

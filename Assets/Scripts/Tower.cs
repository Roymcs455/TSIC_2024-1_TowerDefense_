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
    public float fireRate = 1.5f;
    [SerializeField] towerType type = towerType.Archer;
    private float nextFireTime =0.0f;
    private SphereCollider sphereCollider;
    private float CurrentDamage = 10;
    private float firingRange = 10;

    public LineRenderer line;
    
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = firingRange;
        line = GetComponent<LineRenderer>();        
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
        if(objective != null)
            muzzle.LookAt(objective.transform);
    }

    void DeleteLine()
    {
        line.positionCount=0;
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
                line.positionCount=2;
                line.SetPosition(0,muzzle.position);
                line.SetPosition(1,objective.transform.position);
                Invoke("DeleteLine",0.2f);
                objective.SendMessage("GetDamagedByPercent",CurrentDamage/100.0f);

            break;
            default:
            break;
        }
        
    }
    void IncreaseDamage()
    {
        CurrentDamage += CurrentDamage*.1f;
    }
    void IncreaseAttackSpeed()
    {
        fireRate += fireRate*.1f;
    }
    void IncreaseFiringRange()
    {
        firingRange += firingRange*.1f;
    }
    
}

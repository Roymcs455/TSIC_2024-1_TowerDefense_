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
    [SerializeField] towerType type = towerType.Archer;
    private float nextFireTime =0.0f;
    private SphereCollider sphereCollider;
    private float firingRange = 10f;
    private float CurrentDamage = 10f;
    private float fireRate = 1.5f;
    public float BaseDamage = 10f;
    public float BaseRange = 10f;
    public float BaseFireRate = 1.5f;

    public int upgradePrice = 20; 
    public LineRenderer line;
    public int GetPrice(){return upgradePrice;}
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = firingRange;
        line = GetComponent<LineRenderer>();    
        Restart();    
    }
    public void Restart()
    {
        fireRate = BaseFireRate;
        CurrentDamage = BaseDamage;
        firingRange = BaseRange;
        upgradePrice = 20;
        sphereCollider.radius = firingRange;
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
        {
            muzzle.LookAt(objective.transform);
            if(type == towerType.Mage)
            {
                line.positionCount=2;
                line.SetPosition(0,muzzle.position);
                line.SetPosition(1,objective.transform.position);
            }
        }
        else
        {
            DeleteLine();
        }
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
                objective.SendMessage("GetDamagedByPercent",CurrentDamage/100.0f);

            break;
            default:
            break;
        }
        
    }
    public void Upgrade()
    {
        GameManager.playerScore -=upgradePrice;
        upgradePrice+=upgradePrice;
        switch(type)
        {
            case towerType.Archer:
                IncreaseFiringRange();
                IncreaseAttackSpeed();
            break;
            case towerType.Mage:
                IncreaseDamage();
                IncreaseDamage();
            break;
            case towerType.Bomb:
                IncreaseAttackSpeed();  
                IncreaseDamage();              
            break;
            default:
            break;
        }
    }
    void IncreaseDamage()
    {
        CurrentDamage += CurrentDamage*.60f;
    }
    void IncreaseAttackSpeed()
    {
        fireRate += fireRate*2.0f;
    }
    void IncreaseFiringRange()
    {
        firingRange += firingRange*0.8f;
        sphereCollider.radius = firingRange;
    }
    
}

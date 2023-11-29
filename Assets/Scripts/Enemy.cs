using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum EnemyType
    {
        Skeleton,
        Ogre,
        Wolf,
        Flayer,
        Imp,
        Dragon

    }
    [SerializeField]EnemyType type = EnemyType.Skeleton;
    [SerializeField] private Transform movePositionTransform;
    
    public float baseHealth = 100.0f;
    private float currentHealth;
    public float movementSpeed=1.0f;
    [SerializeField] private float baseDamage= 10.0f;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rg;

    private void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        rg = GetComponent<Rigidbody>();
        currentHealth = baseHealth;
    }
    private void Update()
    {
        
        navMeshAgent.destination = movePositionTransform.position;
    }
    public void SetObjective(Transform objective)
    {
        movePositionTransform = objective;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag=="Castle")
        {
            Debug.Log("Entra enemigo");
            collision.SendMessage("GetDamaged",baseDamage,SendMessageOptions.DontRequireReceiver);
            Die();
        }
    }
    void GetDamaged(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }
    void GetDamagedByPercent(float percent)
    {
        currentHealth = (1-percent)*currentHealth;
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }
    void Die ()
    {
        switch (type)
        {
            case EnemyType.Skeleton:
                GameManager.Instance.AddScore(8);
            break;
            case EnemyType.Ogre:
                GameManager.Instance.AddScore(30);
            break;
            case EnemyType.Wolf:
                GameManager.Instance.AddScore(2);
            break;
            default:
            break;
        }
        Destroy(gameObject);
    }
}

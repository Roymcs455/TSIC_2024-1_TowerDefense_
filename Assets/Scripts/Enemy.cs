using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    
    private float health;
    private float movementSpeed=1.0f;
    private float damage= 10.0f;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rg;

    private void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        rg = GetComponent<Rigidbody>();
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
            collision.SendMessage("GetDamaged",damage,SendMessageOptions.DontRequireReceiver);
            Die();
        }
    }
    void Die ()
    {
        Destroy(gameObject);
    }
}

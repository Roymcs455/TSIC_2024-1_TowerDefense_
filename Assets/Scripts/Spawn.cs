using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Castle;
    [SerializeField] GameObject enemy;
    [SerializeField] float enemySpeed=3.0f;
    
    public void SpawnEnemy(){
        if(Castle!= null)
        {
            GameObject instantiatedEnemy = Instantiate(enemy,transform.position,transform.rotation);
            instantiatedEnemy.SendMessage("SetObjective",Castle);
            instantiatedEnemy.GetComponent<NavMeshAgent>().speed=enemySpeed;

        }
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            SpawnEnemy();
        }
    }
    
}

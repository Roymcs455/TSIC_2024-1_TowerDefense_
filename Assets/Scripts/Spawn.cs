using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Castle;
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject ogre;
    [SerializeField] GameObject wolf;
    [SerializeField] GameObject flayer;
    [SerializeField] GameObject imp;
    [SerializeField] GameObject dragon;
    [SerializeField] float enemySpeed=3.0f;
    
    void Start()
    {
        InvokeRepeating("SpawnEnemy",5.0f,1.0f);
    }
    public void SpawnEnemy(){
        if(Castle!= null)
        {
            GameObject instantiatedEnemy = Instantiate(ogre,transform.position,transform.rotation);
            instantiatedEnemy.SendMessage("SetObjective",Castle);
            //instantiatedEnemy.GetComponent<NavMeshAgent>().speed=enemySpeed;

        }
    }
    void Update()
    {
        // if(Input.GetKeyDown("space"))
        // {
        //     SpawnEnemy();
        // }


    }
    
}

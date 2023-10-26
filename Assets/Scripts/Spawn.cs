using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Castle;
    [SerializeField] GameObject enemy;
    
    public void SpawnEnemy(){
        GameObject instantiatedEnemy = Instantiate(enemy,transform.position,transform.rotation);
        instantiatedEnemy.SendMessage("SetObjective",Castle);
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            SpawnEnemy();
        }
    }
    
}

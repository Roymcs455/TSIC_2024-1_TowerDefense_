using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public bool test = false;
    [SerializeField] Transform[] spawnPoints = new Transform[4];
    [SerializeField] Transform Castle;
    [SerializeField] GameObject[] enemies;
    //[SerializeField] float enemySpeed=3.0f;
    
    [SerializeField]private int enemiesPerWave;
    private float nextSpawnTime = 0.0f;
    private int enemyCount = 0;

    
    void Start()
    {
        if(test)
            InvokeRepeating("SpawnEnemy",5.0f,1.0f);
        
        
    }
    public void SpawnEnemy(){
        if(Castle!= null)
        {
            int randomSpawnPoint = Random.Range(0,4);
            int randomEnemy = Random.Range(0,100);
            GameObject instantiatedEnemy;
            if(randomEnemy <= 33)
            {
                //Instanciar Wolf
                instantiatedEnemy = Instantiate(enemies[0],spawnPoints[randomSpawnPoint].position,spawnPoints[randomSpawnPoint].rotation);
            }
            else if(randomEnemy <= 85)
            {
                //Instanciar Skeleton
                instantiatedEnemy = Instantiate(enemies[1],spawnPoints[randomSpawnPoint].position,spawnPoints[randomSpawnPoint].rotation);
            }
            else 
            {
                //Instanciar Ogre;
                instantiatedEnemy = Instantiate(enemies[2],spawnPoints[randomSpawnPoint].position,spawnPoints[randomSpawnPoint].rotation);
            }
            instantiatedEnemy.SendMessage("SetObjective",Castle);
            enemyCount++;
            if(enemyCount>= 20)
            {
                enemyCount = 0;
                IncreaseSpawnRate();
            }


        }
    }
    public void IncreaseSpawnRate()
    {
        GameManager.spawnRate+= .2f;
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            SpawnEnemy();
        }
        if(GameManager.currentState==GameManager.GameStates.Playing)
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + 1f / GameManager.spawnRate; // Calculate next spawn time based on spawn rate
            }

    }
    
}

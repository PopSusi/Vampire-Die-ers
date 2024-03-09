using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Managers
{
    public GameObject playerObj;
    public PlayerController playerRef;
    
    public GameObject[] Enemies;
    public GameObject[] Obstacles;
    public Vector3 size;
    

    
    public Density myDens;
    private void Start()
    {
        InitalizeVariables();
    }

    protected void SpawnEnemy()
    {
        GameObject enemyToBeSpawned = Enemies[Random.Range(0, Enemies.Length-1)];
        float distance = Random.Range( 13, 25 );
        float angle = Random.Range( -Mathf.PI, Mathf.PI );
        Vector3 spawnPos = playerObj.transform.position;
        spawnPos += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        GameObject spawnedEnemy = Instantiate(enemyToBeSpawned, spawnPos, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyBase>().playerObj = playerObj;
    }

    protected void SpawnObstacles()
    {
        int obsCountRange = (int) (size.x * size.y * 2 * densMods[myDens]);
        for (int i = 0; i < Random.Range(obsCountRange, obsCountRange); i++) //For between min and max obstacles
        {
            int j = Random.Range(0, Obstacles.Length); //Randomly choose Obstacles by Level
            Debug.Log($"{j} out of {Obstacles.Length -1}");
            Vector3 spawnObsPos = new Vector3(Random.Range(-size.x, size.x),Random.Range(-size.y, size.y), 0);
                    //Create spawn point in bounds of Negative and Positive size
            GameObject obst = Instantiate(Obstacles[j], spawnObsPos, Quaternion.identity); //Instantiate
            obst.tag = "Obstacles";
        }
    }

    protected void InitalizeVariables()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRef = playerObj.GetComponent<PlayerController>();
    }
}

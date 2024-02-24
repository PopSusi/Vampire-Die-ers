using System;
using System.Collections;
using System.Collections.Generic;
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
    public Vector2 obsCountRange;
    private void Start()
    {
        InitalizeVariables();
    }

    public void SpawnEnemy()
    {
        GameObject enemyToBeSpawned = Enemies[Random.Range(0, Enemies.Length-1)];
        float distance = Random.Range( 13, 25 );
        float angle = Random.Range( -Mathf.PI, Mathf.PI );
        Vector3 spawnPos = playerObj.transform.position;
        spawnPos += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        GameObject spawnedEnemy = Instantiate(enemyToBeSpawned, spawnPos, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyBase>().playerObj = playerObj;
    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < Random.Range(obsCountRange.x, obsCountRange.y); i++) //For between min and max obstacles
        {
            int j = Random.Range(0, Obstacles.Length); //Randomly choose Obstacles by Level
            Debug.Log($"{j} out of {Obstacles.Length -1}");
            Vector3 spawnObsPos = new Vector3(Random.Range(-size.x, size.x),Random.Range(-size.y, size.y), 0);
                    //Create spawn point in bounds of Negative and Positive size
            Instantiate(Obstacles[j], spawnObsPos, Quaternion.identity); //Instantiate
        }
    }

    public void InitalizeVariables()
    {
        Debug.Log("ALIVE");
        Debug.Log("ALIVE");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRef = playerObj.GetComponent<PlayerController>();
    }
}

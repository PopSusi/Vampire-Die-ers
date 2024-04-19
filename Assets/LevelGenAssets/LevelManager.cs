using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Managers
{
    public GameObject[] Enemies;
    public GameObject[] Obstacles;
    public Vector3 size;
    

    
    public Density myDens;
    private void Start()
    {
    }

    protected void SpawnEnemy()
    {
        float distance = Random.Range( 13, 25 );
        float angle = Random.Range( -Mathf.PI, Mathf.PI );
        Vector3 spawnPos = PlayerController.instance.gameObject.transform.position;
        spawnPos += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        Instantiate(Enemies[Random.Range(0, Enemies.Length - 1)], spawnPos, Quaternion.identity);
    }

    protected void SpawnObstacles()
    {
        try
        {
            int obsCountRange = (int)(size.x * size.y * 2 * densMods[myDens]);
            for (int i = 0; i < Random.Range(obsCountRange, obsCountRange); i++) //For between min and max obstacles
            {
                int j = Random.Range(0, Obstacles.Length); //Randomly choose Obstacles by Level
                //Debug.Log($"{j} out of {Obstacles.Length - 1}");
                Vector3 spawnObsPos = new Vector3(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y), 0);
                //Create spawn point in bounds of Negative and Positive size
                GameObject obst = Instantiate(Obstacles[j], spawnObsPos, Quaternion.identity); //Instantiate
                obst.tag = "Obstacles";
            }
        }
        catch (ArgumentOutOfRangeException error)
        {
            Debug.LogError(error.Message + "Obstacles is 0.");
        }
    }
}

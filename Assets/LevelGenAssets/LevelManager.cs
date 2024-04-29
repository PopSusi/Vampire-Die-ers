using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class LevelManager : Managers
{
    public GameObject[] Enemies;
    public GameObject[] Obstacles;
    public Vector3 size;
    public static LevelManager instance;
    //Terrain Occlusion
    public static List<GameObject> Terrain = new List<GameObject>();
    float timeDown = 1800;
    float timeUp = 0;
    [SerializeField] TextMeshProUGUI timeText;
    EnemyType[] enemyTypes = new EnemyType[GameManager.EnemyCount];

    public Density myDens;
    protected void Start()
    {
        StartCoroutine("Starting");
        InitialSpawnObstacles();
        StartCoroutine("EnemyLoop");
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
        
    }

    protected void FixedUpdate(){
        DistanceCheck();
        timeDown -= Time.fixedDeltaTime;
        timeDown += Time.fixedDeltaTime;
        if (timeDown == 0)
        {
            //WIN GAME
        }
        UpdateTime();
    }
    private void UpdateTime()
    {
        int mins = (int)(timeDown / 60);
        int secs = (int)(timeDown % 60);
        string outString = mins.ToString(); //Minutes
        outString += ":";
        outString += secs.ToString(); //Seconds
        timeText.text = outString;

    }
    IEnumerator EnemyLoop()
    {
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            SpawnEnemy();
        }
        yield return new WaitForSeconds(Random.Range(3f, 8f));
;    }

    protected void SpawnEnemy()
    {
        float distance = Random.Range( 13, 25 );
        float angle = Random.Range( -Mathf.PI, Mathf.PI );
        Vector3 spawnPos = PlayerController.instance.gameObject.transform.position;
        spawnPos += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

        GameObject enemyBasePrefab = Resources.Load<GameObject>("EnemyBase");

        GameObject enemy = Instantiate(enemyBasePrefab, spawnPos, Quaternion.identity);
        enemyTypes = Resources.LoadAll<EnemyType>("EnemyTypes");
        EnemyType chosen = enemyTypes[Random.Range(0, GameManager.EnemyCount - 1)];
        enemy.GetComponent<EnemyBase>().type = chosen;
        //Debug.Log("yay " + chosen);
    }

    protected void InitialSpawnObstacles()
    {
        try
        {
            int obsCountRange = (int)(size.x * size.y * 2 * densMods[myDens]);
            for (int i = 0; i < Random.Range(obsCountRange, obsCountRange); i++) //For between min and max obstacles
            {
                NewTerrain();
                
            }
        }
        catch (ArgumentOutOfRangeException error)
        {
            Debug.LogError(error.Message + "Obstacles is 0.");
        } 
    }
    public void SpawnNewTerrain(int count)
    {
        int j = Random.Range(0, Obstacles.Length); //Randomly choose Obstacles by Level
        //Debug.Log($"{j} out of {Obstacles.Length - 1}");
        float x = Random.Range(-size.x, size.x);
        float y = Random.Range(-size.y, size.y);
        x += x >= 0 ? 2 : -2;
        y += y >= 0 ? 1 : -1;
        Vector3 spawnObsPos = new Vector3(x, y, 0) + PlayerController.instance.transform.position;
        //Create spawn point in bounds of Negative and Positive size
        GameObject obst = Instantiate(Obstacles[j], spawnObsPos, Quaternion.identity); //Instantiate
        obst.tag = "Obstacles";
        Terrain.Add(obst);
    }

    private void NewTerrain()
    {
        int j = Random.Range(0, Obstacles.Length); //Randomly choose Obstacles by Level
        //Debug.Log($"{j} out of {Obstacles.Length - 1}");
        float x = Random.Range(-size.x, size.x);
        float y = Random.Range(-size.y, size.y);
        Vector3 spawnObsPos = new Vector3(x, y, 0) + PlayerController.instance.transform.position;
        //Create spawn point in bounds of Negative and Positive size
        GameObject obst = Instantiate(Obstacles[j], spawnObsPos, Quaternion.identity); //Instantiate
        obst.tag = "Obstacles";
        Terrain.Add(obst);
    }

    private void DistanceCheck()
    {
        //Debug.Log(Terrain.Count);
        var tempArray = Terrain.ToArray();
        foreach (GameObject GO in tempArray)
        {

            float mag = Vector3.Distance(PlayerController.instance.transform.position,GO.transform.position);
            int i = 0;
            if (mag > 13f)
            {
                Terrain.Remove(GO);
                Destroy(GO);
                i++;
                SpawnNewTerrain(i);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Desert : LevelManager
{
    // Start is called before the first frame update
    void Start()
    {
        InitalizeVariables();
        StartCoroutine("Starting");
        SpawnObstacles();
        BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1);
        SpawnEnemy();
    }
}

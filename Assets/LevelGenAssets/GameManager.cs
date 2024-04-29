using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Managers
{
    public bool paused;
    public GameObject pauseScreen;
    public GameObject Desert;

    public const int EnemyCount = 4;
    float time;

    private void Start()
    {
        SelectedLevel = LevelType.Desert;
        AddLevelManager();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void OnPause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    private void AddLevelManager()
    {
        switch (SelectedLevel)
        {
            case LevelType.Forest:
                //gameObject.AddComponent<Forest>(); Not yet implemented
                break;
            case LevelType.Stone:
                //gameObject.AddComponent<Stone>(); Not yet implemented
                break;
            case LevelType.Desert:
            default:
                GameObject level = Instantiate(Desert, this.gameObject.transform); //Childs the Level Prefab
                break;
                
        }
    }
}

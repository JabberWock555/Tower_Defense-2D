using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameObject Level;
    [SerializeField] private int LevelCount;
    [HideInInspector]public int Score;
    private int defaultPlayerHealth = 50;
    private int HighScore;

    public bool Playing = false;
    private int currLevel;
    private Level[] Levels;

    
    void Start()
    {
        Score = 0;
        Levels = new Level[LevelCount];
        for(int i = 0; i < LevelCount; i++)
        {
            Levels[i] = new Level(i);
        }
        currLevel = 0;
        Levels[currLevel].levelStatus = LevelStatus.Level_Unlocked;
        play(false);
    }

    void Update()
    {
        
    }

    public int GetLevelNo()
    {
        return currLevel;
    }

    public LevelStatus GetLevelStatus(int levelNo)
    {
        return Levels[levelNo].levelStatus;
    }

    public int GetEnemyCount(int levelNo)
    {
        return Levels[levelNo].enemyCount;
    }

    public void PlayerHit()
    {
        Score += 2;
    }

    public void LevelComplete()
    {
        uiManager.openLevelCompleteMenu();
        play(false);
        Levels[currLevel].levelStatus = LevelStatus.Level_Completed;
        if (currLevel >= 0 && currLevel < Levels.Length)
        {
            currLevel++;
            Levels[currLevel].levelStatus = LevelStatus.Level_Unlocked;
            if (Score > HighScore) {
                HighScore += Score;
                PlayerPrefs.SetInt("HighScore",HighScore);
            }
        }
    }

    public void LevelFailed()
    {
        uiManager.openLevelFailedMenu();
        play(false);
    }

    public void LoadLevel(int levelNo)
    {
        if (Levels[levelNo].levelStatus != LevelStatus.Level_Locked)
        {
            Score = 0;
            PlayerController.Instance.PlayerHealth(defaultPlayerHealth, defaultPlayerHealth);
            EnemySpawner.Instance.setEnemyCount(Levels[currLevel].enemyCount);
            play(true);

        }
    }

    public void ReloadLevel()
    {
        if (Levels[currLevel].levelStatus != LevelStatus.Level_Locked)
        {
            PlayerController.Instance.PlayerHealth(defaultPlayerHealth, defaultPlayerHealth);
            EnemySpawner.Instance.setEnemyCount(Levels[currLevel].enemyCount);
            EnemySpawner.Instance.reloadSpawn();
            play(true);
        }
    }

    private void play(bool status)
    {
        Playing = status;
        Level.SetActive(status);
    }
}

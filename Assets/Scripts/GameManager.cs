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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private int LevelCount;
    [HideInInspector]public int Score;
    private int defaultPlayerHealth = 100;
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
        UiManager.Instance.openLevelCompleteMenu();
        Playing = false;
        Levels[currLevel].levelStatus = LevelStatus.Level_Completed;
        if (currLevel >= 0 && currLevel < Levels.Length)
        {
            currLevel++;
            Levels[currLevel].levelStatus = LevelStatus.Level_Unlocked;
            if (Score > HighScore) { PlayerPrefs.SetInt("Score", HighScore); }
        }
    }

    public void LevelFailed()
    {
        UiManager.Instance.openLevelFailedMenu();
        Playing = false;
        //Debug.Log("Level Failed");
        //LoadLevel(currLevel);
    }

    public void LoadLevel(int levelNo)
    {
        if (Levels[levelNo].levelStatus != LevelStatus.Level_Locked)
        {
            PlayerController.Instance.PlayerHealth(defaultPlayerHealth, defaultPlayerHealth);
            EnemySpawner.Instance.setEnemyCount(Levels[currLevel].enemyCount);
            Playing = true;
        }
    }

}

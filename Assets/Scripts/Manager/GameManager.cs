using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;
using System.IO;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public int CurrentStageIndex;
    public List<bool> StageClearStatus = new List<bool>();
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }
    public PlayerManager playerManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("게임매니저 어웨이크 호출");
            SaveManager saveManager = FindObjectOfType<SaveManager>();
            if (saveManager != null)
            {
                Debug.Log("게임매니저에서 세이브 매니저 찾음");
                saveManager.LoadClearStatus(); 
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void InitializeStageClearStatus()
    {
        int totalStages = 2;  // 총 스테이지 수에 맞게 조절
        for (int i = 0; i < totalStages; i++)
        {
            StageClearStatus.Add(false);
        }
    }

    public void SetStageClear(int Index, bool isClear)
    {
        while (StageClearStatus.Count <= Index)
        {
            StageClearStatus.Add(false);
        }
        StageClearStatus[Index] = isClear;
    }

    public void OnclickLoadGame()
    {
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        if (saveManager != null)
        {
            saveManager.LoadGame();
        }
        else
        {
            Debug.LogWarning("Save manager not found.");
        }
    }
}


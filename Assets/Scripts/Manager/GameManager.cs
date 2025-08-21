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
        if (playerManager == null) return;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SaveManager saveManager = FindObjectOfType<SaveManager>();
            if (saveManager != null)
            {
                saveManager.LoadClearStatus();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
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
}


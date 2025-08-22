using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;
using System.IO;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public int CurrentStageIndex;
    public List<bool> StageClearStatus = new List<bool>();
    public SaveManager saveManager;
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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (saveManager == null)
            {
                saveManager = transform.AddComponent<SaveManager>();
            }
            saveManager.LoadClearStatus();
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


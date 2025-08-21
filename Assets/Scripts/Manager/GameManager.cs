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
            Debug.Log("���ӸŴ��� �����ũ ȣ��");
            SaveManager saveManager = FindObjectOfType<SaveManager>();
            if (saveManager != null)
            {
                Debug.Log("���ӸŴ������� ���̺� �Ŵ��� ã��");
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
        int totalStages = 2;  // �� �������� ���� �°� ����
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


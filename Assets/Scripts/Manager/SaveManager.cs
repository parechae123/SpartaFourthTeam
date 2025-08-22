using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // ���� ���� ������ Ŭ����
    [System.Serializable]
    public class SaveData
    {
        public int currentStageIndex; // ���� �÷��� ���� �������� �ε���
        public List<bool> StageClearStatus; // �������� Ŭ���� ���� ����Ʈ
    }

    public Transform playerTransform;
    public List<BoxObject> grabbedItems; // �����̴� ������ ����Ʈ

    // ���� �Լ�
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        saveData.currentStageIndex = GameManager.Instance.CurrentStageIndex;
        saveData.StageClearStatus = new List<bool>(GameManager.Instance.StageClearStatus);
        Debug.Log("���� �������� �ε���: " + saveData.currentStageIndex);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);

        Debug.Log("���� ���� �Ϸ�: " + Application.persistentDataPath);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("����� ������ �����ϴ�: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        GameManager.Instance.CurrentStageIndex = saveData.currentStageIndex;
        GameManager.Instance.StageClearStatus = new List<bool>(saveData.StageClearStatus);
        Debug.Log("���� �ε� �Ϸ�");
    }

    public void LoadClearStatus()
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("����� ������ �����ϴ�: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        GameManager.Instance.StageClearStatus = new List<bool>(saveData.StageClearStatus);
        Debug.Log("�������� Ŭ���� ���� �ε� �Ϸ�");
    }
    public void OnStageClear()
    {
        Debug.Log("�������� Ŭ���� �̺�Ʈ �߻�");
        int clearedStage = GameManager.Instance.CurrentStageIndex;
        Debug.Log("Ŭ����� �������� �ε���: " + clearedStage);
        GameManager.Instance.SetStageClear(clearedStage, true);

        SaveManager saveManager = FindObjectOfType<SaveManager>();
        if (saveManager != null)
        {
            saveManager.SaveGame();
        }
        else
        {
         Debug.LogWarning("���̺� �Ŵ����� ã�� �� �����ϴ�.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class Save : MonoBehaviour
{
    // ���� ���� ������ Ŭ����
    [System.Serializable]
    public class SaveData
    {
        public PlayerData playerData; // �÷��̾� ��ġ ������
        public List<ItemData> grabbedItems; // �����̴� ������ ����Ʈ
        public int currentStageIndex; // ���� �÷��� ���� �������� �ε���
        public List<bool> StageClearStatus; // �������� Ŭ���� ���� ����Ʈ
    }

    [System.Serializable]
    public class PlayerData
    {
        public float playerPositionX;
        public float playerPositionY;
        public float playerPositionZ;
    }

    [System.Serializable]
    public class ItemData
    {
        public float itemPositionX;
        public float itemPositionY;
        public float itemPositionZ;
    }

    public Transform playerTransform;
    public List<BoxObject> grabbedItems; // �����̴� ������ ����Ʈ

    // ���� �Լ�
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        // �÷��̾� ������ ����
        PlayerData pData = new PlayerData
        {
            playerPositionX = playerTransform.position.x,
            playerPositionY = playerTransform.position.y,
            playerPositionZ = playerTransform.position.z,
        };
        saveData.playerData = pData;

        // ������ ������ ����
        saveData.grabbedItems = new List<ItemData>();
        foreach (var item in grabbedItems)
        {
            ItemData iData = new ItemData
            {
                itemPositionX = item.transform.position.x,
                itemPositionY = item.transform.position.y,
                itemPositionZ = item.transform.position.z
            };
            saveData.grabbedItems.Add(iData);
        }
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

        if (playerTransform == null)
        {
            Debug.LogError("playerTransform�� �Ҵ�Ǿ� ���� �ʽ��ϴ�!");
            return;  // ���� ��� �� �̻� �������� ����
        }
        // �÷��̾� ��ġ ����
        playerTransform.position = new Vector3(
            saveData.playerData.playerPositionX,
            saveData.playerData.playerPositionY,
            saveData.playerData.playerPositionZ
        );
      


        //�����̴� ������ ��ġ ����
        for (int i = 0; i < grabbedItems.Count && i < saveData.grabbedItems.Count; i++)
        {
            grabbedItems[i].transform.position = new Vector3(
                saveData.grabbedItems[i].itemPositionX,
               saveData.grabbedItems[i].itemPositionY,
                saveData.grabbedItems[i].itemPositionZ
            );
        }
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
        GameManager.Instance.SetStageClear(clearedStage, true);

        Save saveManager = FindObjectOfType<Save>();
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

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
        public PlayerData playerData;
        public List<ItemData> grabbedItems;
    }

    [System.Serializable]
    public class PlayerData
    {
        public int playerHealth;
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
    public Condition playerHealth; // �÷��̾� HP ��ũ��Ʈ ����
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
            playerHealth = PlayerManager.Instance.player.Health
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

        // JSON ����ȭ �� ����
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

        // �÷��̾� ��ġ �� HP ����
        playerTransform.position = new Vector3(
            saveData.playerData.playerPositionX,
            saveData.playerData.playerPositionY,
            saveData.playerData.playerPositionZ
        );

        
         PlayerManager.Instance.player.Health = saveData.playerData.playerHealth;

        // �����̴� ������ ��ġ ����
        for (int i = 0; i < grabbedItems.Count && i < saveData.grabbedItems.Count; i++)
        {
            grabbedItems[i].transform.position = new Vector3(
                saveData.grabbedItems[i].itemPositionX,
                saveData.grabbedItems[i].itemPositionY,
                saveData.grabbedItems[i].itemPositionZ
            );
        }

        Debug.Log("���� �ε� �Ϸ�");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class Save : MonoBehaviour
{
    // 게임 저장 데이터 클래스
    [System.Serializable]
    public class SaveData
    {
        public PlayerData playerData; // 플레이어 위치 데이터
        public List<ItemData> grabbedItems; // 움직이는 아이템 리스트
        public int currentStageIndex; // 현재 플레이 중인 스테이지 인덱스
        public List<bool> StageClearStatus; // 스테이지 클리어 상태 리스트
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
    public List<BoxObject> grabbedItems; // 움직이는 아이템 리스트

    // 저장 함수
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        // 플레이어 데이터 생성
        PlayerData pData = new PlayerData
        {
            playerPositionX = playerTransform.position.x,
            playerPositionY = playerTransform.position.y,
            playerPositionZ = playerTransform.position.z,
        };
        saveData.playerData = pData;

        // 아이템 데이터 생성
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
        Debug.Log("현재 스테이지 인덱스: " + saveData.currentStageIndex);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);

        Debug.Log("게임 저장 완료: " + Application.persistentDataPath);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("저장된 파일이 없습니다: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        if (playerTransform == null)
        {
            Debug.LogError("playerTransform이 할당되어 있지 않습니다!");
            return;  // 널일 경우 더 이상 실행하지 않음
        }
        // 플레이어 위치 적용
        playerTransform.position = new Vector3(
            saveData.playerData.playerPositionX,
            saveData.playerData.playerPositionY,
            saveData.playerData.playerPositionZ
        );
      


        //움직이는 아이템 위치 적용
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
        Debug.Log("게임 로드 완료");
    }

    public void LoadClearStatus()
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("저장된 파일이 없습니다: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        GameManager.Instance.StageClearStatus = new List<bool>(saveData.StageClearStatus);
        Debug.Log("스테이지 클리어 상태 로드 완료");
    }
    public void OnStageClear()
    {
        Debug.Log("스테이지 클리어 이벤트 발생");
        int clearedStage = GameManager.Instance.CurrentStageIndex;
        GameManager.Instance.SetStageClear(clearedStage, true);

        Save saveManager = FindObjectOfType<Save>();
        if (saveManager != null)
        {
            saveManager.SaveGame();
        }
        else
        {
         Debug.LogWarning("세이브 매니저를 찾을 수 없습니다.");
        }
    }
}

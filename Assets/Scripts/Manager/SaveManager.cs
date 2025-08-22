using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // 게임 저장 데이터 클래스
    [System.Serializable]
    public class SaveData
    {
        public int currentStageIndex; // 현재 플레이 중인 스테이지 인덱스
        public List<bool> StageClearStatus; // 스테이지 클리어 상태 리스트
    }

    public Transform playerTransform;
    public List<BoxObject> grabbedItems; // 움직이는 아이템 리스트

    // 저장 함수
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

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
        Debug.Log("클리어된 스테이지 인덱스: " + clearedStage);
        GameManager.Instance.SetStageClear(clearedStage, true);

        SaveManager saveManager = FindObjectOfType<SaveManager>();
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

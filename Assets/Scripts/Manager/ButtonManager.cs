using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private bool SetSave = false;
    public int CurrentinDex;
    void Awake()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

            if (sceneName.StartsWith("Stage"))  // 스테이지 씬 이름 규칙 필터링
            {
                stageSceneNames.Add(sceneName);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {
                ResumGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        PlayerManager.Instance.player.isUiOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        PlayerManager.Instance.player.isUiOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Restarting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    List<string> stageSceneNames = new List<string>();

    

    // 다음 스테이지 불러오기
    public void NextStage()
    {
        int nextStageIndex = GameManager.Instance.CurrentStageIndex + 1;
        if (nextStageIndex >= stageSceneNames.Count)
        {
            Debug.Log("마지막 스테이지입니다.");
            SceneManager.LoadScene("StartScene");
            return;
        }
        GameManager.Instance.CurrentStageIndex = nextStageIndex;
        SceneManager.LoadScene(stageSceneNames[nextStageIndex]);
    }
}

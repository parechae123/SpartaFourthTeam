using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private bool SetSave = false;

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
    public void NextStage()
    {
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextStageIndex = currentIndex + 1;
        Debug.Log($"현재 씬 인덱스: {currentIndex}, 다음 씬 인덱스: {nextStageIndex}");
        Debug.Log($"빌드 세팅 씬 개수: {SceneManager.sceneCountInBuildSettings}");

        SceneManager.LoadScene(nextStageIndex);
        // 빌드 설정에 있는 씬 수를 넘어가지 않도록 확인
        if (nextStageIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("마지막 스테이지입니다. 메인 화면으로 이동합니다.");
            SceneManager.LoadScene("StartScene");  // 메인 씬 이름
            return;
        }
    }
}

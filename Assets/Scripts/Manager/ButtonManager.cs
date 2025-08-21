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
        Debug.Log($"���� �� �ε���: {currentIndex}, ���� �� �ε���: {nextStageIndex}");
        Debug.Log($"���� ���� �� ����: {SceneManager.sceneCountInBuildSettings}");

        SceneManager.LoadScene(nextStageIndex);
        // ���� ������ �ִ� �� ���� �Ѿ�� �ʵ��� Ȯ��
        if (nextStageIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("������ ���������Դϴ�. ���� ȭ������ �̵��մϴ�.");
            SceneManager.LoadScene("StartScene");  // ���� �� �̸�
            return;
        }
    }
}

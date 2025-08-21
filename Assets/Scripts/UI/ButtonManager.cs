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
    //public void NextStage()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    GameManager.Instance.CurrentStageIndex = nextStageIndex;

    //    // ¥Ÿ¿Ω æ¿ ∑ŒµÂ
    //    SceneManager.LoadScene(nextStageIndex);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMain : MonoBehaviour
{
    public GameObject Panel;
    public Save saveManager;

    public void OnClickYesSave()
    {
        saveManager.SaveGame();
        GotoMainMenu();
    }

    public void OnClickNoSave()
    {
        GotoMainMenu();
    }
    
    public void OnClickExitMain()
    {
        Panel.SetActive(true);
    }

    void GotoMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("StartScene");
    }


   
}

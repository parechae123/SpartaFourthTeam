using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Clear Triggered");
            SaveManager saveManager = GameObject.FindObjectOfType<SaveManager>();
            if (saveManager != null)
            {
                Debug.Log("Ŭ���� �������� ����Ϸ�");
                saveManager.OnStageClear(); // Save the game state
            }
            else
            {
                Debug.LogWarning("Save manager not found, skipping save.");
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("ClearScene");
        }
    }
}



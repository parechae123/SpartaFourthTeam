using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public string ClearScene;

    // 임시 조건: C 키를 누르면 클리어 씬으로 이동
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(ClearScene);
        }
    }
}
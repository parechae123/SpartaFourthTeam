using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public string ClearScene;

    // �ӽ� ����: C Ű�� ������ Ŭ���� ������ �̵�
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(ClearScene);
        }
    }
}
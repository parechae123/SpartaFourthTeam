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
            SceneManager.LoadScene(ClearScene);
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button stageButton;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private GameObject panelStage;
    private string mainSceneName = "MainScene"; // 나중에 씬이름 변경이 수정 필요

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (panelStage == null)
        {
            GameObject canvas = GameObject.FindWithTag("Canvas");
            if (canvas != null)
            {
                Transform t = canvas.transform.Find("Panel_Stage");
                if (t != null) panelStage = t.gameObject;
            }
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.BindStartMenu(
                startButton,
                exitButton,
                stageButton,
                soundSlider,
                panelStage,
                mainSceneName
            );
        }
        else
        {
            Debug.LogWarning("UIManager 인스턴스를 찾지 못했습니다.");
        }
    }
}

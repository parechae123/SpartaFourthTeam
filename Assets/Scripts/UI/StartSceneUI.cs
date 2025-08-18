using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [Header("Main Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button stageMenuButton;

    [Header("Stage Buttons")]
    [SerializeField] Button stageButton1;
    [SerializeField] Button stageButton2;
    [SerializeField] Button stageButton3;
    [SerializeField] Button closeButton;

    [Header("Others")]
    [SerializeField] Slider soundSlider;
    [SerializeField] GameObject panelStage;

    [Tooltip("스테이지 씬 이름")]
    [SerializeField] string[] stageSceneNames;

    void OnEnable()  => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!panelStage)
        {
            var canvas = GameObject.FindWithTag("Canvas");
            if (canvas)
            {
                var t = canvas.transform.Find("Panel_Stage");
                if (t) panelStage = t.gameObject;
            }
        }

        if (UIManager.Instance)
        {
            UIManager.Instance.BindStartMenu(
                startButton,
                exitButton,
                stageMenuButton,
                closeButton,
                soundSlider,
                panelStage,
                new[] { stageButton1, stageButton2, stageButton3 },
                stageSceneNames
            );
        }
        else
        {
            Debug.LogWarning("⚠ UIManager 인스턴스를 찾지 못했습니다.");
        }
    }
}

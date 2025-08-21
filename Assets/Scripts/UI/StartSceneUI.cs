using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [Tooltip("스테이지 씬 이름 (index: 각 스테이지 버튼)")]
    [SerializeField] string[] stageSceneNames;

    SlideEffect transition;
    RectTransform panelStageRT;

    void Awake()
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

        transition = FindObjectOfType<SlideEffect>();
        panelStageRT = panelStage ? panelStage.GetComponent<RectTransform>() : null;

        if (panelStage) panelStage.SetActive(false);
    }

    void OnEnable()
    {
        if (startButton)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartClicked);
        }

        if (exitButton)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(OnExitClicked);
        }

        if (stageMenuButton)
        {
            stageMenuButton.onClick.RemoveAllListeners();
            stageMenuButton.onClick.AddListener(OnStageMenuClicked);
        }

        if (closeButton)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseClicked);
        }

        WireStageButton(stageButton1, 0);
        WireStageButton(stageButton2, 1);
        WireStageButton(stageButton3, 2);

        if (soundSlider)
        {
            soundSlider.onValueChanged.RemoveAllListeners();

            if (UIManager.Instance != null)
                soundSlider.value = AudioManager.Instance.GetVolume();

            soundSlider.onValueChanged.AddListener(OnSoundChanged);
        }
    }

    void OnDisable()
    {
        if (startButton)      startButton.onClick.RemoveAllListeners();
        if (exitButton)       exitButton.onClick.RemoveAllListeners();
        if (stageMenuButton)  stageMenuButton.onClick.RemoveAllListeners();
        if (closeButton)      closeButton.onClick.RemoveAllListeners();

        if (stageButton1)     stageButton1.onClick.RemoveAllListeners();
        if (stageButton2)     stageButton2.onClick.RemoveAllListeners();
        if (stageButton3)     stageButton3.onClick.RemoveAllListeners();

        if (soundSlider)      soundSlider.onValueChanged.RemoveAllListeners();
    }

    public void OnStartClicked()
    {
        LoadStageByIndex(0);
    }

    public void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnStageMenuClicked()
    {
        transition.Slide(panelStageRT, SlideEffect.Dir.Right, 0.6f, show: true);
    }

    public void OnCloseClicked()
    {
        transition.Slide(panelStageRT, SlideEffect.Dir.Left, 0.6f, show: false);
    }

    void OnSoundChanged(float v)
    {
        AudioManager.Instance.SetVolume(v);
    }

    public void OnStageClicked(int index)
    {
        LoadStageByIndex(index);
    }

    void WireStageButton(Button btn, int index)
    {
        if (!btn) return;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => OnStageClicked(index));
    }

    void LoadStageByIndex(int index)
    {
        AudioManager.Instance.StopBGM();
        if (stageSceneNames == null || index < 0 || index >= stageSceneNames.Length || string.IsNullOrEmpty(stageSceneNames[index]))
        {
            Debug.LogWarning($"Scene{index + 1} 범위초과");
            return;
        }
        SceneManager.LoadScene(stageSceneNames[index]);
    }
}

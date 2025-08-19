using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    Button _startBtn, _exitBtn, _stageMenuBtn, _closeBtn;
    Slider _soundSlider;
    GameObject _panelStage;
    Button[] _stageButtons;
    string[] _stageSceneNames;   // 각 스테이지의 씬 이름
    private SlideEffect transition;

    void Awake()
    {
        if (transition == null) transition = FindObjectOfType<SlideEffect>();
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BindStartMenu(
        Button startBtn,
        Button exitBtn,
        Button stageMenuBtn,
        Button closeBtn,
        Slider soundSlider,
        GameObject panelStage,
        Button[] stageButtons,
        string[] stageSceneNames
    )
    {
        _startBtn = startBtn;
        _exitBtn = exitBtn;
        _stageMenuBtn = stageMenuBtn;
        _closeBtn = closeBtn;
        _soundSlider = soundSlider;
        _panelStage = panelStage;
        _stageButtons = stageButtons ?? Array.Empty<Button>();
        _stageSceneNames = stageSceneNames ?? Array.Empty<string>();

        if (_panelStage) _panelStage.SetActive(false);

        Wire(_startBtn, OnStartClicked);
        Wire(_exitBtn, OnExitClicked);
        Wire(_stageMenuBtn, OnStageMenuClicked);
        Wire(_closeBtn, OnCloseClicked);

        if (_soundSlider)
        {
            _soundSlider.onValueChanged.RemoveAllListeners();
            _soundSlider.value = AudioManager.Instance.Volume;
            _soundSlider.onValueChanged.AddListener(OnSoundChanged);
        }

        for (int i = 0; i < _stageButtons.Length; i++)
        {
            int idx = i;
            Wire(_stageButtons[idx], () => OnStageClicked(idx));
        }
    }
    void OnStartClicked() //처음부터 버튼 누를경우 첫번째 스테이지 연결
    {
        if (_stageSceneNames != null && _stageSceneNames.Length > 0 && !string.IsNullOrEmpty(_stageSceneNames[0]))
        {
            AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(_stageSceneNames[0]);
        }
        else
        {
            Debug.Log($"Scene1 이름이 설정되지 않았습니다.");
        }
    }

    void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnStageMenuClicked()
    {
        if (_panelStage && transition)
        {
            var rt = _panelStage.GetComponent<RectTransform>();
            transition.Slide(rt, SlideEffect.Dir.Right, 0.6f);
        }
    }

    void OnCloseClicked()
    {
        if (_panelStage && transition)
        {
            var rt = _panelStage.GetComponent<RectTransform>();
            transition.Slide(rt, SlideEffect.Dir.Left, 0.6f, show: false);
        }
    }

    void OnStageClicked(int index)
    {
        if (_panelStage == null) return;
        AudioManager.Instance.StopBGM();

        if (_stageSceneNames != null && index >= 0 && index < _stageSceneNames.Length &&
            !string.IsNullOrEmpty(_stageSceneNames[index]))
        {
            SceneManager.LoadScene(_stageSceneNames[index]);
        }
        else
        {
            Debug.Log($"Scene{index + 1} 이름이 설정되지 않았습니다.");
        }
    }

    void OnSoundChanged(float v) => AudioManager.Instance.SetVolume(v);

    public void CloseStagePanel() => OnCloseClicked();

    static void Wire(Button btn, UnityAction action)
    {
        if (!btn) return;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action);
    }
}

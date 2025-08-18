using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Button _startBtn;
    private Button _exitBtn;
    private Button _stageBtn;
    private Slider _soundSlider;
    private GameObject _panelStage;
    private string _mainSceneName;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BindStartMenu(
        Button startBtn,
        Button exitBtn,
        Button stageBtn,
        Slider soundSlider,
        GameObject panelStage,
        string mainSceneName)
    {
        _startBtn = startBtn;
        _exitBtn  = exitBtn;
        _stageBtn = stageBtn;
        _soundSlider = soundSlider;
        _panelStage  = panelStage;
        _mainSceneName = mainSceneName;

        if (_panelStage != null) _panelStage.SetActive(false);

        // 다시 메인으로 돌아왔을 경우를 대비 해서 수동연결
        if (_startBtn != null)
        {
            _startBtn.onClick.RemoveAllListeners();
            _startBtn.onClick.AddListener(OnStartClicked);
        }
        if (_exitBtn != null)
        {
            _exitBtn.onClick.RemoveAllListeners();
            _exitBtn.onClick.AddListener(OnExitClicked);
        }
        if (_stageBtn != null)
        {
            _stageBtn.onClick.RemoveAllListeners();
            _stageBtn.onClick.AddListener(OnStageClicked);
        }
        if (_soundSlider != null)
        {
            _soundSlider.onValueChanged.RemoveAllListeners();


            _soundSlider.value = AudioManager.Instance.Volume;
            _soundSlider.onValueChanged.AddListener(OnSoundChanged);
        }
    }

    public void OnStartClicked()
    {
        AudioManager.Instance.StopBGM();
        SceneManager.LoadScene(_mainSceneName);
    }

    public void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnStageClicked()
    {
        if (_panelStage != null) _panelStage.SetActive(true);
    }

    public void OnSoundChanged(float v)
    {
        AudioManager.Instance.SetVolume(v);
    }

    public void CloseStagePanel()
    {
        if (_panelStage != null) _panelStage.SetActive(false);
    }
}

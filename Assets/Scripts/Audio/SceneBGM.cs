using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip;

    private void Start()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.PlayBGM(bgmClip);
    }
}

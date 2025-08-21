using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    public float Volume { get; private set; } = 1f;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (bgmSource == null)
            bgmSource = gameObject.AddComponent<AudioSource>();

        bgmSource.volume = Volume;
        bgmSource.loop = true;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (!clip) return;
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void SetVolume(float value)
    {
        Volume = Mathf.Clamp01(value);
        bgmSource.volume = Volume;
    }
    public float GetVolume()
    {
        return bgmSource ? bgmSource.volume : Volume;
    }
}

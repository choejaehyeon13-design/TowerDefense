using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource audioSource;

    [Header("BGM Clips")]
    public AudioClip menuBGM;
    public AudioClip stageBGM;
    public AudioClip victoryBGM;
    public AudioClip defeatBGM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBGM(menuBGM);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" || scene.name == "SelectStages")
        {
            PlayBGM(menuBGM);
        }
        else if (scene.name == "Stage1")
        {
            PlayBGM(stageBGM); 
            // 게임 중 무음으로 하고 싶으면 StopBGM(); 사용
        }

    }

    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
            return;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    public void SetMute(bool isMute)
    {
        audioSource.mute = isMute;
    }
}
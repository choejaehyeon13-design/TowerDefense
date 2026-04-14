using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource audioSource;
    public AudioClip bgmClip;

    void Awake()
    {

        // ▶ 싱글톤 (중복 방지)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            
            
        }
        else
        {
            Destroy(gameObject);
        }   }
        void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        Debug.Log("BGM START PLAY: " + audioSource.isPlaying);
    }
        // 🎚️ 볼륨
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    // 🔇 음소거
    public void SetMute(bool isMute)
    {
        audioSource.mute = isMute;
    }
    
}

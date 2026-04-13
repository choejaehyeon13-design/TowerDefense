using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    private AudioSource audioSource;
    private float prevVolume = 1f; // 음소거 해제용

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🎚️ 볼륨 조절
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // 🔇 음소거 ON/OFF
    public void ToggleMute(bool isMute)
    {
        if (isMute)
        {
            prevVolume = audioSource.volume;
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = prevVolume;
        }
    }

    // 현재 볼륨 가져오기 (UI 초기화용)
    public float GetVolume()
    {
        return audioSource.volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource sfxSource;
    public AudioClip clickSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSound);
    }
    public void SetVolume(float value)
    {
        sfxSource.volume = value;
    }

    public void SetMute(bool isMute)
    {
        sfxSource.mute = isMute;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource audioSource;
    public AudioClip clickSound;

    void Awake()
    {
        instance = this;
    }

    // 🔊 버튼 클릭 소리
    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
    
}

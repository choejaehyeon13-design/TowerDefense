using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    void Start()
    {
        float currentVolume = BGMManager.instance.GetVolume();

        // 🔥 이벤트 제거 (핵심)
        volumeSlider.onValueChanged.RemoveAllListeners();
        muteToggle.onValueChanged.RemoveAllListeners();

        // 값 세팅
        volumeSlider.value = currentVolume;
        muteToggle.isOn = (currentVolume == 0f);

        // 다시 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);
    }

    void OnVolumeChanged(float value)
    {
        BGMManager.instance.SetVolume(value);
    }

    void OnMuteChanged(bool isMute)
    {
        BGMManager.instance.ToggleMute(isMute);
    }
}

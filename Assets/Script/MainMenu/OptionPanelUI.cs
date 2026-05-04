using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OptionPanelUI : MonoBehaviour
{
    public GameObject optionPanel;
    public Slider volumeSlider;
    public Toggle muteToggle;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolume);
        muteToggle.onValueChanged.AddListener(OnMute);
    }

    void OnVolume(float v)
    {
        BGMManager.instance.SetVolume(v);
        SFXManager.instance.SetVolume(v);
    }

    void OnMute(bool m)
    {
        BGMManager.instance.SetMute(m);
        SFXManager.instance.SetMute(m);
    }

    // ▶ 옵션 닫기 버튼
    public void CloseOption()
    {
        SFXManager.instance.PlayClick();
        optionPanel.SetActive(false);
    }

    // ▶ 옵션 안 Play 버튼
    public void OnClickPlay()
    {
        SFXManager.instance.PlayClick();
        SceneManager.LoadScene("SelectStages");
    }
}

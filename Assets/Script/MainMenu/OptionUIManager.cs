using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject optionPanel;

    // 옵션 열기
    public void OpenOption()
    {
        optionPanel.SetActive(true);
    }

    // 옵션 닫기
    public void CloseOption()
    {
        optionPanel.SetActive(false);
    }
}

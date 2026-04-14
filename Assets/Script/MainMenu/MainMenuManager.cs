using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject optionPanel;

    // ▶ Play 버튼
    public void OnClickPlay()
    {
        SFXManager.instance.PlayClick();
        SceneManager.LoadScene("SelectStages");
    }
    

    // ▶ Option 버튼
    public void OnClickOption()
    {
        SFXManager.instance.PlayClick();
        optionPanel.SetActive(true);
    }

    // ▶ Quit 버튼
    public void OnClickQuit()
    {
        SFXManager.instance.PlayClick();
        Debug.Log("게임 종료");

        // ▶ 빌드된 게임에서 종료
        Application.Quit();

        // ▶ 유니티 에디터에서 플레이 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


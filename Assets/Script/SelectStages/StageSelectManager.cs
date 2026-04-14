using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public GameObject difficultyPanel; // 난이도 UI
    public GameObject lockedStagePanel;

    private int selectedStage;

    // 스테이지 버튼 클릭
    public void OnClickStage(int stageNumber)
    {
        SFXManager.instance.PlayClick();
        selectedStage = stageNumber;
        difficultyPanel.SetActive(true);
    }
    // 잠긴 스테이지 버튼
    public void OnClickLockedStage()
    {
        SFXManager.instance.PlayClick();
        lockedStagePanel.SetActive(true);
    }

    // 난이도 버튼 클릭 (Easy / Normal / Hard)
    public void OnClickDifficulty()
    {
        SFXManager.instance.PlayClick();
        string sceneName = "Stage" + selectedStage;
        SceneManager.LoadScene(sceneName);
    }
    // 뒤로가기 (패널 닫기)
    public void CloseLockedPanel()
    {
        SFXManager.instance.PlayClick();
        lockedStagePanel.SetActive(false);
    }
    //메인메뉴로 돌아가기
    public void GoToMainMenu()
    {
        SFXManager.instance.PlayClick();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

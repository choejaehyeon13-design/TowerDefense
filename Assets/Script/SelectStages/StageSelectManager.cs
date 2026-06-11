using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public GameObject difficultyPanel;
    public GameObject lockedStagePanel;

    private int selectedStage;

    // 모든 스테이지 버튼 클릭
    public void OnClickStage(int stageNumber)
    {
        SFXManager.instance.PlayClick();

        selectedStage = stageNumber;

        // 지금은 Stage1밖에 없으므로 모든 스테이지가 난이도 선택으로 이동
        difficultyPanel.SetActive(true);
    }

    // 잠긴 스테이지 버튼도 Stage1로 연결
    // 잠긴 스테이지 버튼
    public void OnClickLockedStage()
    {
        SFXManager.instance.PlayClick();

        // 잠금 안내 패널 표시
        lockedStagePanel.SetActive(true);
}
    // 난이도 버튼 클릭
    public void OnClickDifficulty()
    {
        SFXManager.instance.PlayClick();

        // 어떤 스테이지를 눌러도 Stage1 실행
        SceneManager.LoadScene("Stage1");
    }

    // 잠김 패널 닫기
    public void CloseLockedPanel()
    {
        SFXManager.instance.PlayClick();
        lockedStagePanel.SetActive(false);
    }

    // 메인메뉴로 돌아가기
    public void GoToMainMenu()
    {
        SFXManager.instance.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }
}

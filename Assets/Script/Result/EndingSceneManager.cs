using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// 엔딩 결과 화면을 관리하는 클래스
public class EndingSceneManager : MonoBehaviour
{
    // 결과 배경 이미지 관련 변수
    [Header("Result Background")]
    public Image resultBackground;      // 결과 배경 이미지
    public Sprite clearBackground;      // 승리 배경
    public Sprite failBackground;       // 패배 배경

    // 결과 텍스트 UI
    [Header("Result Text")]
    public TMP_Text titleText;          // 승리/패배 제목
    public TMP_Text scoreText;          // 점수 표시
    public TMP_Text waveText;           // 웨이브 표시
    public TMP_Text playTimeText;       // 플레이 시간 표시

    // 버튼 UI
    [Header("Button")]
    public Button retryButton;          // 다시 시작 버튼
    public Button mainMenuButton;       // 메인 메뉴 버튼
    public Button quitButton;           // 게임 종료 버튼

    // 이동할 씬 이름
    [Header("Scene Name")]
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenuScene";

    // 게임 시작 시 실행
    void Start()
    {
        // 결과 화면 표시
        ShowResult();

        // 버튼 클릭 이벤트 연결
        retryButton.onClick.AddListener(RetryGame);
        mainMenuButton.onClick.AddListener(GoMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    // 게임 결과를 화면에 출력하는 함수
    void ShowResult()
    {
        // 클리어 여부 확인
        if (GameResultData.isClear)
        {
            titleText.text = "VICTORY!";
            resultBackground.sprite = clearBackground;
            BGMManager.instance.PlayBGM(BGMManager.instance.victoryBGM);
        }
        else
        {
            titleText.text = "DEFEAT";
            resultBackground.sprite = failBackground;
            BGMManager.instance.PlayBGM(BGMManager.instance.defeatBGM);
        }

        // 결과 데이터 출력
        scoreText.text = "" + GameResultData.score;
        waveText.text = "" + GameResultData.wave;
        playTimeText.text = "" + FormatTime(GameResultData.playTime);
    }

    // 플레이 시간을 MM:SS 형식으로 변환
    string FormatTime(float time)
    {
        int minute = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);

        return minute.ToString("00") + ":" + second.ToString("00");
    }

    // 다시 시작 버튼 클릭 시 Stage1 씬으로 이동
    void RetryGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    // 메인 메뉴 버튼 클릭 시 MainMenu 씬으로 이동
    void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 게임 종료 함수
    void QuitGame()
    {
    #if UNITY_EDITOR
        // 유니티 에디터에서 실행 중일 경우 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        // 빌드된 게임 종료
        Application.Quit();
    #endif
    }

    // 매 프레임마다 실행
    
}
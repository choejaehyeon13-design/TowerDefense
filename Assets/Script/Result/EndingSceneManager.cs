using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
   [Header("Result Background")]
    public Image resultBackground;
    public Sprite clearBackground;
    public Sprite failBackground;

    [Header("Result Text")]
    public TMP_Text titleText;
    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text playTimeText;

    [Header("Button")]
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;

    [Header("Scene Name")]
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenuScene";

    void Start()
    {
        ShowResult();

        retryButton.onClick.AddListener(RetryGame);
        mainMenuButton.onClick.AddListener(GoMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }
    

    void ShowResult()
    {
        if (GameResultData.isClear)
        {
            titleText.text = "VICTORY!";
            resultBackground.sprite = clearBackground;
        }
        else
        {
            titleText.text = "DEFEAT";
            resultBackground.sprite = failBackground;
        }

        scoreText.text = "" + GameResultData.score;
        waveText.text = "" + GameResultData.wave;
        playTimeText.text = "" + FormatTime(GameResultData.playTime);
    }

    string FormatTime(float time)
    {
        int minute = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);

        return minute.ToString("00") + ":" + second.ToString("00");
    }

    void RetryGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    void Update()
{
    if (Input.GetKeyDown(KeyCode.V))
    {
        GameResultData.isClear = true;
        GameResultData.score = 1500;
        GameResultData.wave = 10;
        GameResultData.playTime = 320;

        SceneManager.LoadScene("ResultScene");
    }
    if (Input.GetKeyDown(KeyCode.D))
    {
        GameResultData.isClear = false;
        GameResultData.score = 900;
        GameResultData.wave = 6;
        GameResultData.playTime = 180;

        SceneManager.LoadScene("ResultScene");
    }
}
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("점수 / 생명")]
    public int score = 0;
    public int life = 10;
    public int maxLife = 10;

    [Header("UI 텍스트")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;

    [Header("플레이어 체력바")]
    public Image lifeFillImage;

    [Header("게임 오버 UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    private float lastTakeLifeTime = -1f;
    private float lastAddScoreTime = -1f;

    private bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        life = maxLife;

        UpdateUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver)
        {
            return;
        }

        // 점수가 너무 빠르게 중복으로 올라가는 것을 방지
        if (Time.time - lastAddScoreTime < 0.1f)
        {
            return;
        }

        lastAddScoreTime = Time.time;

        score += amount;

        UpdateUI();
    }

    public void TakeLife(int amount)
    {
        if (isGameOver)
        {
            return;
        }

        // 생명이 너무 빠르게 중복으로 깎이는 것을 방지
        if (Time.time - lastTakeLifeTime < 0.1f)
        {
            return;
        }

        lastTakeLifeTime = Time.time;

        life -= amount;

        if (life < 0)
        {
            life = 0;
        }

        UpdateUI();

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }

        if (lifeText != null)
        {
            lifeText.text = "Life : " + life + " / " + maxLife;
        }

        if (lifeFillImage != null)
        {
            lifeFillImage.fillAmount = (float)life / maxLife;
        }
    }

    private void GameOver()
    {
        isGameOver = true;


        Stage1ResultManager resultManager = FindObjectOfType<Stage1ResultManager>();

        if (resultManager != null)
        {
            resultManager.GoDefeat();
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
        }

        // 게임을 완전히 멈추고 싶으면 사용
        // Time.timeScale = 0f;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
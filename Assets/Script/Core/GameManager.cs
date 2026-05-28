using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int life = 10;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private float lastTakeLifeTime = -1f;
    private float lastAddScoreTime = -1f;

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        if (Time.time - lastTakeLifeTime < 0.1f) return;
        lastTakeLifeTime = Time.time;
        score += amount;
        UpdateUI();
    }

    public void TakeLife(int amount)
    {
        if (isGameOver) return;
        if (Time.time - lastTakeLifeTime < 0.1f) return;
        lastTakeLifeTime = Time.time;

        life -= amount;
        if (life < 0) life = 0;
        UpdateUI();
        if (life <= 0) GameOver();
    }

    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;

        if (lifeText != null)
            lifeText.text = "Life : " + life;
    }

    void GameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "GAME OVER";
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
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

        score += amount;
        UpdateUI();
    }

    public void LoseLife(int amount)
    {
        if (isGameOver) return;

        life -= amount;

        if (life < 0)
            life = 0;

        UpdateUI();

        if (life <= 0)
        {
            GameOver();   //  여기서 호출
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;

        if (lifeText != null)
            lifeText.text = "Life : " + life;
    }

    void GameOver()   
    {
        isGameOver = true;

        Debug.Log("GAME OVER 실행됨");  // 확인용

        Time.timeScale = 0f;

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
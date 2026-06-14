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
        // maxLife가 0 이하이면 오류 방지를 위해 기본값 10으로 보정
        if (maxLife <= 0)
        {
            maxLife = 10;
        }

        life = maxLife;

        if (lifeFillImage == null)
        {
            Debug.LogWarning("GameManager에 lifeFillImage가 연결되지 않았습니다. Fill 오브젝트의 Image를 넣어주세요.");
        }

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

        // life가 0보다 작아지거나 maxLife보다 커지지 않도록 제한
        life = Mathf.Clamp(life - amount, 0, maxLife);

        UpdateUI();

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void HealLife(int amount)
    {
        if (isGameOver)
        {
            return;
        }

        // 회복 기능이 필요할 때 사용 가능
        life = Mathf.Clamp(life + amount, 0, maxLife);

        UpdateUI();
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
            // 체력 비율 계산
            float lifeRatio = (float)life / maxLife;

            // fillAmount는 0~1 사이 값만 사용
            lifeFillImage.fillAmount = Mathf.Clamp01(lifeRatio);
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
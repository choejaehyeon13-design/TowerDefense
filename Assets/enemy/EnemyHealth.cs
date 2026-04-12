using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int scoreValue = 10;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("적 시작 체력: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("데미지 받음: " + damage + " / 남은 체력: " + currentHealth);

        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            Debug.Log("적 사망");
            Destroy(gameObject);
        }
    }
}
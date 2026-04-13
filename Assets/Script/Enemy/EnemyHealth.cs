using UnityEngine;

// 적 체력을 관리하는 스크립트
public class EnemyHealth : MonoBehaviour
{
    // 적 최대 체력
    public int maxHealth = 3;

    // 적을 잡았을 때 얻는 점수
    public int scoreValue = 10;

    // 현재 체력
    private int currentHealth;

    void Start()
    {
        // 시작할 때 현재 체력을 최대 체력으로 설정
        currentHealth = maxHealth;
    }

    // 데미지를 받는 함수
    public void TakeDamage(int damage)
    {
        // 데미지만큼 체력 감소
        currentHealth -= damage;

        // 체력이 0 이하가 되면 적 사망
        if (currentHealth <= 0)
        {
            // 점수 증가
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            // 적 삭제
            Destroy(gameObject);
        }
    }
}
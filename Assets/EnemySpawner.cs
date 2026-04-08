using UnityEngine;

// 적 생성 스크립트
public class EnemySpawner : MonoBehaviour
{
    // 생성할 적 프리팹
    public GameObject enemyPrefab;

    // 적 생성 간격
    public float spawnInterval = 2f;

    // 시간 누적용 변수
    private float timer = 0f;

    void Update()
    {
        // 게임오버 상태면 더 이상 적 생성 안 함
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

        // 시간 누적
        timer += Time.deltaTime;

        // 생성 간격이 지나면 적 생성
        if (timer >= spawnInterval)
        {
            Instantiate(enemyPrefab);
            timer = 0f;
        }
    }
}
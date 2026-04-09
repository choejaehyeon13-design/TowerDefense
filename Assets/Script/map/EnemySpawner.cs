using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject enemyPrefab;

    [Header("스폰 위치")]
    public Transform spawnPoint;

    [Header("웨이포인트 경로")]
    public Waypoint waypoint;

    [Header("스폰 설정")]
    public int numOfEnemy = 10;
    public float spawnInterval = 1.5f;

    private bool isSpawning = false;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        for (int i = 0; i < numOfEnemy; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null || waypoint == null)
        {
            Debug.LogError("EnemySpawner: 필요한 오브젝트가 연결되지 않았습니다.");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        if (enemyMove != null)
        {
            enemyMove.SetWayPoints(waypoint.GetWayPoints());
        }
    }
}
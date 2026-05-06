using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("생성할 적 프리팹")]
    public GameObject enemyPrefab;

    
    [Header("웨이포인트 생성기")]
    public WaypointGenerator waypointGenerator;

    
    [Header("스폰 포인트 2개")]
    public Transform[] spawnPoints;

    
    [Header("스폰 설정")]
    public int enemyCount = 10;
    public float spawnInterval = 1.5f;

    private Transform[] leftWayPoints;
    private Transform[] rightWayPoints;

    private IEnumerator Start()
    {
        while (
            waypointGenerator == null ||
            waypointGenerator.leftWayPoints == null ||
            waypointGenerator.leftWayPoints.Length == 0 ||
            waypointGenerator.rightWayPoints == null ||
            waypointGenerator.rightWayPoints.Length == 0
        )
        {
            yield return null;
        }

        leftWayPoints = waypointGenerator.leftWayPoints;
        rightWayPoints = waypointGenerator.rightWayPoints;

        Debug.Log("경로 연결 완료: " + leftWayPoints.Length + ", " + rightWayPoints.Length);

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab이 연결되지 않았습니다.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length < 2)
        {
            Debug.LogError("Spawn Points 2개를 연결해야 합니다.");
            return;
        }

        int randomIndex = Random.Range(0, 2);
        Transform selectedSpawnPoint = spawnPoints[randomIndex];

        GameObject enemy = Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        Debug.Log("적 생성됨: " + selectedSpawnPoint.name);

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        if (enemyMove == null)
        {
            Debug.LogError("Enemy Prefab에 EnemyMove가 없습니다.");
            return;
        }

        if (randomIndex == 0)
        {
            enemyMove.SetWayPoints(leftWayPoints);
        }
        else
        {
            enemyMove.SetWayPoints(rightWayPoints);
        }
    }
}
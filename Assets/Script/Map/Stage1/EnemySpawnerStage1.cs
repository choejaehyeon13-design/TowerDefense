using System.Collections;
using UnityEngine;

public class EnemySpawner_Stage1 : MonoBehaviour
{
    [Header("생성할 적 프리팹")]
    public GameObject enemyPrefab;

    [Header("웨이포인트 생성기")]
    public WaypointGenerator_Stage1 waypointGenerator;

    [Header("스폰 포인트")]
    public Transform[] spawnPoints;

    [Header("스폰 설정")]
    public int enemyCount = 10;
    public float spawnInterval = 1.5f;

    private Transform[] wayPoints;

    private IEnumerator Start()
    {
        if (waypointGenerator == null)
        {
            Debug.LogError("WaypointGenerator가 연결되지 않았습니다.");
            yield break;
        }

        waypointGenerator.GenerateWaypoints();

        yield return null;

        wayPoints = waypointGenerator.wayPoints;

        if (wayPoints == null || wayPoints.Length == 0)
        {
            Debug.LogError("EnemySpawner_Stage1 웨이포인트가 비어있습니다.");
            yield break;
        }

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

        if (spawnPoints == null || spawnPoints.Length < 1)
        {
            Debug.LogError("Spawn Point 1개를 연결해야 합니다.");
            return;
        }

        Transform selectedSpawnPoint = spawnPoints[0];

        GameObject enemy = Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        if (enemyMove == null)
        {
            Debug.LogError("Enemy Prefab에 EnemyMove가 없습니다.");
            return;
        }

        enemyMove.SetWayPoints(wayPoints);

        Debug.Log("적 생성됨: " + selectedSpawnPoint.name);
    }
}
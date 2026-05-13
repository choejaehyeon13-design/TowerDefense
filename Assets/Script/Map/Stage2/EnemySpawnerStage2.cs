using System.Collections;
using UnityEngine;

public class EnemySpawner_Stage2 : MonoBehaviour
{
    [Header("생성할 적 프리팹")]
    public GameObject enemyPrefab;


    
    [Header("웨이포인트 생성기")]
    public WaypointGenerator_Stage2 waypointGenerator;

    
    [Header("스폰 포인트 2개")]
    public Transform[] spawnPoints;



    [Header("스폰 설정")]
    public int enemyCount = 10;
    public float spawnInterval = 1.5f;

    private Transform[] leftWayPoints;
    private Transform[] rightWayPoints;

    private IEnumerator Start()
    {
        if (waypointGenerator == null)
        {
            Debug.LogError("WaypointGenerator가 연결되지 않았습니다.");
            yield break;
        }

        // 핵심: 스폰 전에 웨이포인트를 먼저 확실히 생성
        waypointGenerator.GenerateWaypoints();

        yield return null;

        leftWayPoints = waypointGenerator.leftWayPoints;
        rightWayPoints = waypointGenerator.rightWayPoints;

        if (leftWayPoints == null || leftWayPoints.Length == 0 ||
            rightWayPoints == null || rightWayPoints.Length == 0)
        {
            Debug.LogError("웨이포인트 생성 실패: left/right 경로가 비어 있습니다.");
            yield break;
        }

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

        Debug.Log("적 생성됨: " + selectedSpawnPoint.name);
    }
}
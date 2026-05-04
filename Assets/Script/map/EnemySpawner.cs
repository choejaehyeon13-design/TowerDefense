using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject enemyPrefab;

    [Header("적 스폰 위치")]
    public Transform spawnPoint;

    [Header("웨이포인트 부모")]
    public Transform wayPoint;

    [Header("적 스폰 설정")]
    public int enemyCount = 10;
    public float spawnInterval = 1.5f;

    private Transform[] wayPoints;

    private void Start()
    {
        LoadWayPoints();
        StartCoroutine(SpawnEnemies());
    }

    void LoadWayPoints()
    {
        if (wayPoint == null)
        {
            Debug.LogError("wayPoint가 연결되지 않았습니다.");
            return;
        }

        int count = wayPoint.childCount;
        wayPoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            wayPoints[i] = wayPoint.GetChild(i);
        }
    }

    IEnumerator SpawnEnemies()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab이 연결되지 않았습니다.");
            yield break;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("spawnPoint가 연결되지 않았습니다.");
            yield break;
        }

        if (wayPoints == null || wayPoints.Length == 0)
        {
            Debug.LogError("WayPoint가 없습니다.");
            yield break;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyMove move = enemy.GetComponent<EnemyMove>();
            if (move != null)
            {
                move.SetWayPoints(wayPoints);
            }
            else
            {
                Debug.LogError("적 프리팹에 EnemyMove 스크립트가 없습니다.");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
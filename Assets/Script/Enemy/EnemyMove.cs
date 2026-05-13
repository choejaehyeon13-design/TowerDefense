using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("적 이동 속도")]
    public float speed = 2f;

    private Transform[] wayPoints;
    private int currentIndex = 0;

    // EnemySpawner에서 경로를 넣어줄 때 사용하는 함수
    public void SetWayPoints(Transform[] points)
    {
        wayPoints = points;
        currentIndex = 0;
    }

    private void Update()
    {
        // 경로가 없으면 이동하지 않음
        if (wayPoints == null || wayPoints.Length == 0)
        {
            return;
        }

        // 모든 웨이포인트를 지나면 적 삭제
        if (currentIndex >= wayPoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        // 현재 목표 웨이포인트
        Transform target = wayPoints[currentIndex];

        // 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // 목표 지점에 거의 도착하면 다음 웨이포인트로 이동
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player 태그를 가진 오브젝트와 닿으면 삭제
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 도착! 적 삭제");
            Destroy(gameObject);
        }
    }
}
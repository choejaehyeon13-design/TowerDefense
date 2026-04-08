using System.Collections.Generic;
using UnityEngine;

// 적 이동을 담당하는 스크립트
public class EnemyMove : MonoBehaviour
{
    // 적 이동 속도
    public float speed = 2f;

    // 적 회전 속도
    public float rotationSpeed = 10f;

    // waypoint들을 저장할 리스트
    private List<Transform> waypoints = new List<Transform>();

    // 현재 향하고 있는 waypoint 번호
    private int currentIndex = 0;

    // 스프라이트 반전을 위한 SpriteRenderer
    private SpriteRenderer sr;

    void Start()
    {
        // SpriteRenderer 가져오기
        sr = GetComponent<SpriteRenderer>();

        // waypoint 부모 오브젝트 찾기
        GameObject parent = GameObject.Find("Tilemap-WayPoint");

        // 부모가 없으면 오류 출력 후 종료
        if (parent == null)
        {
            Debug.LogError("Tilemap-WayPoint를 찾을 수 없습니다.");
            return;
        }

        // Tilemap-WayPoint의 자식들 중 WayPoint 스크립트가 붙은 것만 리스트에 저장
        foreach (Transform child in parent.transform)
        {
            if (child.GetComponent<WayPoint>() != null)
            {
                waypoints.Add(child);
            }
        }

        // waypoint가 하나도 없으면 오류 출력
        if (waypoints.Count == 0)
        {
            Debug.LogError("WayPoint가 없습니다.");
            return;
        }

        // 적 시작 위치를 첫 번째 waypoint 위치로 맞춤
        transform.position = new Vector3(
            waypoints[0].position.x,
            waypoints[0].position.y,
            0f
        );

        // 다음 waypoint부터 이동 시작
        currentIndex = 1;
    }

    void Update()
    {
        // waypoint 끝까지 도착했으면 목적지 도착 처리
        if (currentIndex >= waypoints.Count)
        {
            ReachGoal();
            return;
        }

        // 현재 목표 waypoint 위치
        Vector3 targetPos = new Vector3(
            waypoints[currentIndex].position.x,
            waypoints[currentIndex].position.y,
            0f
        );

        // 목표 방향 계산
        Vector3 moveDir = (targetPos - transform.position).normalized;

        // 방향에 따라 회전/반전 처리
        UpdateDirection(moveDir);

        // 현재 위치에서 목표 위치까지 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // waypoint에 충분히 가까워지면 다음 waypoint로 변경
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            currentIndex++;
        }
    }

    // 목적지 도착 시 Life 감소 후 적 삭제
    void ReachGoal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife(1);
        }

        Destroy(gameObject);
    }

    // 이동 방향에 따라 적 회전 및 좌우 반전 처리
    void UpdateDirection(Vector3 dir)
    {
        float angle = 0f;

        // 좌우 이동이 더 큰 경우
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                // 오른쪽 이동
                angle = 0f;
                sr.flipX = false;
            }
            else
            {
                // 왼쪽 이동
                angle = 0f;
                sr.flipX = true;
            }
        }
        else
        {
            // 상하 이동일 때는 좌우 반전 해제
            sr.flipX = false;

            if (dir.y > 0)
            {
                // 위쪽 이동
                angle = 90f;
            }
            else
            {
                // 아래쪽 이동
                angle = -90f;
            }
        }

        // 목표 회전값 생성
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);

        // 부드럽게 회전
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
}
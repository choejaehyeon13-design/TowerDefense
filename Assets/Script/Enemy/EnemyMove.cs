using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("적 이동 속도")]
    public float speed = 2f;

    [Header("애니메이션 상태 이름")]
    [SerializeField] private string downWalkAnim = "D_Walk";
    [SerializeField] private string upWalkAnim = "U_Walk";
    [SerializeField] private string sideWalkAnim = "S_Walk";

    private Transform[] wayPoints;
    private int currentIndex = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private string currentAnimName = "";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // EnemySpawner에서 경로를 넣어줄 때 사용하는 함수
    public void SetWayPoints(Transform[] points)
    {
        wayPoints = points;
        currentIndex = 0;

        // 첫 웨이포인트 위치로 적을 정확히 맞춤
        if (wayPoints != null && wayPoints.Length > 0)
        {
            transform.position = wayPoints[0].position;
        }
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeLife(1);
            }

            Destroy(gameObject);
            return;
        }

        // 현재 목표 웨이포인트 위치
        Vector3 targetPosition = wayPoints[currentIndex].position;

        // 이동 방향 계산
        Vector3 direction = targetPosition - transform.position;

        // 방향에 맞는 애니메이션 실행
        UpdateMoveAnimation(direction);

        // 목표 웨이포인트로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // 목표 지점에 거의 도착하면 다음 웨이포인트로 이동
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            currentIndex++;
        }
    }

    /// <summary>
    /// 이동 방향에 따라 위, 아래, 좌우 애니메이션을 변경하는 함수
    /// </summary>
    private void UpdateMoveAnimation(Vector3 direction)
    {
        if (animator == null)
        {
            return;
        }

        // 거의 이동하지 않는 상태면 애니메이션 변경하지 않음
        if (direction.magnitude < 0.01f)
        {
            return;
        }

        // 좌우 이동이 위아래 이동보다 크면 좌우 애니메이션
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            PlayAnimation(sideWalkAnim);

            // 오른쪽으로 가면 기본 방향
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            // 왼쪽으로 가면 좌우 반전
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        // 위아래 이동이 더 크면 위/아래 애니메이션
        else
        {
            // 위로 이동
            if (direction.y > 0)
            {
                PlayAnimation(upWalkAnim);
            }
            // 아래로 이동
            else if (direction.y < 0)
            {
                PlayAnimation(downWalkAnim);
            }
        }
    }

    /// <summary>
    /// 같은 애니메이션을 매 프레임 반복 실행하지 않도록 처리
    /// </summary>
    private void PlayAnimation(string animName)
    {
        if (currentAnimName == animName)
        {
            return;
        }

        currentAnimName = animName;
        animator.Play(animName);
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
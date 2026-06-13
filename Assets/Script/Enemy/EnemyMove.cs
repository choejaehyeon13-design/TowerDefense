using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("적 이동 속도")]
    public float speed = 2f;

    [Header("애니메이션 상태 이름")]
    [SerializeField] private string downWalkAnim = "D_Walk";
    [SerializeField] private string upWalkAnim = "U_Walk";
    [SerializeField] private string sideWalkAnim = "S_Walk";

    [Header("적 그림 위치 보정")]
    [SerializeField] private Vector3 visualOffset = new Vector3(0f, 0.5f, 0f);

    [Header("그림 오브젝트")]
    [SerializeField] private Transform visualTransform;

    private Transform[] wayPoints;
    private int currentIndex = 0;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private string currentAnimName = "";

    private void Awake()
    {
        if (visualTransform == null)
        {
            Transform foundVisual = transform.Find("SlimeVisual");

            if (foundVisual != null)
            {
                visualTransform = foundVisual;
            }
        }

        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        ApplyVisualOffset();
    }

    public void SetWayPoints(Transform[] points)
    {
        wayPoints = points;
        currentIndex = 0;

        if (wayPoints != null && wayPoints.Length > 0)
        {
            // 웨이포인트 좌표는 절대 변경하지 않음
            transform.position = wayPoints[0].position;
        }
    }

    private void Update()
    {
        if (wayPoints == null || wayPoints.Length == 0)
        {
            return;
        }

        if (currentIndex >= wayPoints.Length)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeLife(1);
            }

            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = wayPoints[currentIndex].position;
        Vector3 direction = targetPosition - transform.position;

        UpdateMoveAnimation(direction);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            currentIndex++;
        }

        ApplyVisualOffset();
    }

    private void LateUpdate()
    {
        ApplyVisualOffset();
    }

    private void ApplyVisualOffset()
    {
        if (visualTransform != null)
        {
            visualTransform.localPosition = visualOffset;
            visualTransform.localScale = Vector3.one;
        }
    }

    private void UpdateMoveAnimation(Vector3 direction)
    {
        if (animator == null)
        {
            return;
        }

        if (direction.magnitude < 0.01f)
        {
            return;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            PlayAnimation(sideWalkAnim);

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }

            if (direction.y > 0)
            {
                PlayAnimation(upWalkAnim);
            }
            else if (direction.y < 0)
            {
                PlayAnimation(downWalkAnim);
            }
        }
    }

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 도착! 적 삭제");
            Destroy(gameObject);
        }
    }
}
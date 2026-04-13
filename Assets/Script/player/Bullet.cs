using UnityEngine;

// 총알 이동 및 적 충돌 처리를 담당하는 스크립트
public class Bullet : MonoBehaviour
{
    // 총알 이동 속도
    public float speed = 4f;

    // 총알 데미지
    public int damage = 1;

    // 총알이 따라갈 목표 적
    private Transform target;

    // 타워가 생성 직후 목표 적을 넣어주는 함수
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        // 목표가 없으면 총알 삭제
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 목표 적 위치 계산
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 0f);

        // 목표 방향 계산
        Vector3 dir = (targetPos - transform.position).normalized;

        // 총알이 이동 방향을 바라보도록 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 총알 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // 적에 충분히 가까워지면 데미지 적용
        if (Vector3.Distance(transform.position, targetPos) < 0.15f)
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
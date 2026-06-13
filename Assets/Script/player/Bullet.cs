using UnityEngine;

// 총알 이동 및 적 공격 처리 스크립트
public class Bullet : MonoBehaviour
{
    [Header("총알 이동 속도")]
    public float speed = 4f;

    [Header("총알 데미지")]
    public int damage = 1;

    [Header("타격 판정 거리")]
    public float hitDistance = 0.15f;

    private Transform target;

    // 타워가 공격할 대상을 넣어주는 함수
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        // 대상이 사라졌으면 총알 삭제
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 0f);

        Vector3 dir = (targetPos - transform.position).normalized;

        // 총알 방향 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 총알 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // 목표 지점에 가까워지면 데미지 처리
        if (Vector3.Distance(transform.position, targetPos) < hitDistance)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 1차: target 오브젝트에서 EnemyHealth 찾기
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

        // 2차: target의 부모에서 EnemyHealth 찾기
        if (enemyHealth == null)
        {
            enemyHealth = target.GetComponentInParent<EnemyHealth>();
        }

        // 3차: target의 자식에서 EnemyHealth 찾기
        if (enemyHealth == null)
        {
            enemyHealth = target.GetComponentInChildren<EnemyHealth>();
        }

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            Debug.Log("총알 명중! 데미지: " + damage);
        }
        else
        {
            Debug.LogWarning("EnemyHealth를 찾지 못했습니다. EnemyHealth가 적 오브젝트에 붙어 있는지 확인하세요.");
        }

        Destroy(gameObject);
    }
}
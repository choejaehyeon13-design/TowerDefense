using UnityEngine;

// 타워 공격을 담당하는 스크립트
public class TowerAttack : MonoBehaviour
{
    // 공격 범위
    public float attackRange = 5f;

    // 공격 간격
    public float attackDelay = 0.5f;

    // 포신 회전 속도
    public float rotateSpeed = 5f;

    // 발사할 총알 프리팹
    public GameObject bulletPrefab;

    // 총알이 생성될 위치
    public Transform firePoint;

    // 포신 오브젝트
    public Transform towerHead;

    // 공격 타이머
    private float timer = 0f;

    void Update()
    {
        // 시간 누적
        timer += Time.deltaTime;

        // 가장 가까운 적 찾기
        GameObject target = FindNearestEnemy();

        // 적이 있으면 포신 회전 + 공격
        if (target != null)
        {
            RotateHeadToTarget(target.transform);

            if (timer >= attackDelay)
            {
                Shoot(target.transform);
                timer = 0f;
            }
        }
    }

    // 범위 안의 가장 가까운 적을 찾는 함수
    GameObject FindNearestEnemy()
    {
        // Enemy 태그가 붙은 적들을 모두 가져옴
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDist = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            // 범위 안에 있으면서 더 가까우면 갱신
            if (dist <= minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    // 총알 발사 함수
    void Shoot(Transform target)
    {
        // 연결이 안 되어 있으면 발사하지 않음
        if (bulletPrefab == null || firePoint == null) return;

        // firePoint 위치에서 총알 생성
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 생성된 총알에 목표 적 전달
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    // 포신만 적 방향으로 회전시키는 함수
    void RotateHeadToTarget(Transform target)
    {
        if (towerHead == null) return;

        // 포신 기준으로 적 방향 계산
        Vector3 dir = target.position - towerHead.position;

        // 각도 계산
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 목표 회전값 생성
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // 포신만 부드럽게 회전
        towerHead.rotation = Quaternion.Lerp(
            towerHead.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    // Scene에서 공격 범위를 확인하기 위한 원 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
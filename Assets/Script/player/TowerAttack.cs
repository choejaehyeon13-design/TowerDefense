using UnityEngine;

// Ÿ�� ������ ����ϴ� ��ũ��Ʈ
public class TowerAttack : MonoBehaviour
{
    // ���� ����
    public float attackRange = 5f;

    // ���� ����
    public float attackDelay = 0.5f;

    // ���� ȸ�� �ӵ�
    public float rotateSpeed = 5f;

    // �߻��� �Ѿ� ������
    public GameObject bulletPrefab;

    // �Ѿ��� ������ ��ġ
    public Transform firePoint;

    // ���� ������Ʈ
    public Transform towerHead;

    // ���� Ÿ�̸�
    private float timer = 0f;

    void Update()
{
    timer += Time.deltaTime;

    GameObject target = FindNearestEnemy();

    if (target == null)
    {
        Debug.Log("타워가 적을 못 찾는 중");
        return;
    }

    Debug.Log("타워가 적 찾음: " + target.name);

    RotateHeadToTarget(target.transform);

    if (timer >= attackDelay)
    {
        Debug.Log("타워 발사!");
        Shoot(target.transform);
        timer = 0f;
    }
}

    // ���� ���� ���� ����� ���� ã�� �Լ�
    GameObject FindNearestEnemy()
    {
        // Enemy �±װ� ���� ������ ��� ������
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDist = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            // ���� �ȿ� �����鼭 �� ������ ����
            if (dist <= minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    // �Ѿ� �߻� �Լ�
    void Shoot(Transform target)
    {
        // ������ �� �Ǿ� ������ �߻����� ����
        if (bulletPrefab == null || firePoint == null) return;

        // firePoint ��ġ���� �Ѿ� ����
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // ������ �Ѿ˿� ��ǥ �� ����
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    // ���Ÿ� �� �������� ȸ����Ű�� �Լ�
    void RotateHeadToTarget(Transform target)
    {
        if (towerHead == null) return;

        // ���� �������� �� ���� ���
        Vector3 dir = target.position - towerHead.position;

        // ���� ���
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // ��ǥ ȸ���� ����
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // ���Ÿ� �ε巴�� ȸ��
        towerHead.rotation = Quaternion.Lerp(
            towerHead.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    // Scene���� ���� ������ Ȯ���ϱ� ���� �� ǥ��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
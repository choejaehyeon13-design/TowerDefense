using UnityEngine;
using System.Collections;

public class ArcherTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float range = 7f;
    public float cooldown = 1f;
    public int damage = 1;

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            Transform target = FindNearestEnemy();

            // 👉 적 없으면 대기
            if (target == null)
            {
                Debug.Log("아쳐타워: 공격 대기중");
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Debug.Log("아쳐타워: 적 발견");

                Shoot(target);

                // 👉 코루틴 쿨타임 처리
                yield return new WaitForSeconds(cooldown);
            }
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearest = null;
        float minDist = range;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);

            if (dist <= minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    void Shoot(Transform target)
    {
        if (projectilePrefab == null) return;

        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }
}
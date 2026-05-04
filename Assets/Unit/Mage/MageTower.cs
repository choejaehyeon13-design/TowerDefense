using UnityEngine;
using System.Collections;

public class MageTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float range = 7f;
    public float cooldown = 0.3f;
    public int damage = 3;

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                Shoot(target);
                yield return new WaitForSeconds(cooldown);
            }
            else
            {
                // 적 없으면 너무 자주 검사 안하게
                yield return new WaitForSeconds(0.1f);
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
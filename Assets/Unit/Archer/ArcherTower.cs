using UnityEngine;
using System.Collections;

public class ArcherTower : MonoBehaviour
{
    [Header("Attack")]
    public GameObject projectilePrefab;
    public float range = 7f;
    public float cooldown = 1f;
    public int damage = 1;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Timing")]
    public float preAttackDelay = 0.15f;
    public float attackDuration = 0.3f;

    private Transform currentTarget;
    private bool isAttacking = false;

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (!isAttacking)
            {
                if (currentTarget == null || !IsTargetValid(currentTarget))
                {
                    currentTarget = FindNearestEnemy();
                }

                if (currentTarget != null)
                {
                    yield return StartCoroutine(Attack(currentTarget));
                    yield return new WaitForSeconds(cooldown);
                }
            }

            yield return null;
        }
    }

    IEnumerator Attack(Transform target)
    {
        isAttacking = true;

        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }

        yield return new WaitForSeconds(preAttackDelay);

        if (target != null && IsTargetValid(target))
        {
            Shoot(target);
        }

        yield return new WaitForSeconds(attackDuration);

        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }

        isAttacking = false;
    }

    bool IsTargetValid(Transform target)
    {
        if (target == null)
            return false;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > range)
            return false;

        if (!target.CompareTag("Enemy"))
            return false;

        return true;
    }

    void Shoot(Transform target)
    {
        if (projectilePrefab == null || target == null)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Projectile p = projectile.GetComponent<Projectile>();

        if (p != null)
        {
            p.SetTarget(target, damage);
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearest = null;
        float minDistance = range;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance <= minDistance)
            {
                minDistance = distance;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }
}
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
    private float lastAttackTime = -999f;

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {

            Shoot(target);
            timer = cooldown;

            Debug.Log("short");

            if (!isAttacking && Time.time >= lastAttackTime + cooldown)
            {
                currentTarget = FindNearestEnemy();

                if (currentTarget != null)
                {
                    SetDirection(currentTarget.position - transform.position);

                    StartCoroutine(AttackSequence());

                    lastAttackTime = Time.time;
                }
                else
                {
                    PlayIdle();
                }
            }

            yield return null;
        }
    }

    IEnumerator AttackSequence()
    {
        isAttacking = true;

        // 활 당기기
        animator.SetInteger("State", 1);

        yield return new WaitForSeconds(preAttackDelay);

        // 공격 모션
        animator.SetInteger("State", 2);

        // 화살 발사
        if (currentTarget != null)
        {
            Shoot(currentTarget);
        }

        yield return new WaitForSeconds(attackDuration);

        // Idle 복귀
        animator.SetInteger("State", 0);

        isAttacking = false;
        currentTarget = null;
    }

    void PlayIdle()
    {
        animator.SetInteger("State", 0);
    }

    void SetDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            animator.SetInteger("Direction", 2);

            spriteRenderer.flipX = dir.x < 0;
        }
        else
        {
            if (dir.y > 0)
            {
                animator.SetInteger("Direction", 1);
            }
            else
            {
                animator.SetInteger("Direction", 0);
            }

            spriteRenderer.flipX = false;

        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearest = null;

        float minDist = range;

        foreach (GameObject enemy in enemies)
        {
            float dist =
                Vector2.Distance(transform.position, enemy.transform.position);

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
        if (projectilePrefab == null)
            return;

        GameObject projectileObj =
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Projectile projectile =
            projectileObj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }
}
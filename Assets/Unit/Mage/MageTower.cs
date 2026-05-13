using UnityEngine;
using System.Collections;

public class MageTower : MonoBehaviour
{
    [Header("Attack")]
    public GameObject projectilePrefab;
    public float range = 7f;
    public float cooldown = 3.33f;
    public int damage = 3;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Attack Timing")]
    public float shootDelay = 0.25f; // 공격 모션 시작 후 투사체 나가는 시간

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
                    PlaySpecial();
                }
            }

            yield return null;
        }
    }

    IEnumerator AttackSequence()
    {
        isAttacking = true;

        animator.SetInteger("State", 1);

        yield return new WaitForSeconds(shootDelay);

        if (currentTarget != null)
        {
            Shoot(currentTarget);
        }

        yield return new WaitForSeconds(0.4f);

        isAttacking = false;
        currentTarget = null;
        animator.SetInteger("State", 0);
    }

    void PlaySpecial()
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
            animator.SetInteger("Direction", dir.y > 0 ? 1 : 0);
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
        if (projectilePrefab == null)
        {
            Debug.LogError("마법사 projectilePrefab 연결 안 됨");
            return;
        }

        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Projectile projectile = projectileObj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
            Debug.Log("마법사 투사체 생성 완료");
        }
        else
        {
            Debug.LogError("투사체 프리팹에 Projectile 또는 MageProjectile 없음");
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarriorTower : MonoBehaviour
{
    public int damage = 3;
    public float cooldown = 1f;

    public float tileSize = 1f;
    public float radius = 0.6f;

    public int maxHitCount = 3;

    public GameObject hitEffectPrefab;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private List<EnemyHealth> currentTargets = new List<EnemyHealth>();
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
                List<EnemyHealth> enemies = GetEnemiesIn3x3();

                if (enemies.Count > 0)
                {
                    SortByDistance(enemies);

                    SetDirection(enemies[0].transform.position - transform.position);
                    PlayAttack(enemies);

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

    void PlayAttack(List<EnemyHealth> enemies)
    {
        currentTargets.Clear();

        int hitCount = Mathf.Min(maxHitCount, enemies.Count);

        for (int i = 0; i < hitCount; i++)
        {
            currentTargets.Add(enemies[i]);
        }

        isAttacking = true;
        animator.SetInteger("State", 1);
    }

    void PlaySpecial()
    {
        animator.SetInteger("State", 0);
    }

    public void AttackHit()
    {
        foreach (EnemyHealth enemy in currentTargets)
        {
            if (enemy == null)
                continue;

            enemy.TakeDamage(damage);
            SpawnEffect(enemy.transform.position);
        }
    }

    public void AttackEnd()
    {
        isAttacking = false;
        currentTargets.Clear();
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
                animator.SetInteger("Direction", 1);
            else
                animator.SetInteger("Direction", 0);

            spriteRenderer.flipX = false;
        }
    }

    List<EnemyHealth> GetEnemiesIn3x3()
    {
        List<EnemyHealth> enemies = new List<EnemyHealth>();

        Vector2 center = transform.position;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 checkPos = center + new Vector2(x * tileSize, y * tileSize);

                Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, radius);

                foreach (Collider2D hit in hits)
                {
                    if (!hit.CompareTag("Enemy"))
                        continue;

                    EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();

                    if (enemyHealth != null && !enemies.Contains(enemyHealth))
                    {
                        enemies.Add(enemyHealth);
                    }
                }
            }
        }

        return enemies;
    }

    void SortByDistance(List<EnemyHealth> enemies)
    {
        enemies.Sort((a, b) =>
        {
            float distA = Vector2.Distance(transform.position, a.transform.position);
            float distB = Vector2.Distance(transform.position, b.transform.position);

            return distA.CompareTo(distB);
        });
    }

    void SpawnEffect(Vector2 pos)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, pos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector2 center = transform.position;
        Gizmos.color = Color.red;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 pos = center + new Vector2(x * tileSize, y * tileSize);
                Gizmos.DrawWireSphere(pos, radius);
            }
        }
    }
}
using UnityEngine;

public class WarriorTower : MonoBehaviour
{
    public int damage = 3;
    public float cooldown = 1f;

    public float tileSize = 1f;
    public float radius = 0.6f;

    public GameObject hitEffectPrefab;

    float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && HasEnemy())
        {
            Attack();
            timer = cooldown;
        }
    }

    bool HasEnemy()
    {
        Vector2 c = transform.position;

        return Check(c) ||
               Check(c + Vector2.up * tileSize) ||
               Check(c + Vector2.down * tileSize) ||
               Check(c + Vector2.left * tileSize) ||
               Check(c + Vector2.right * tileSize);
    }

    bool Check(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, radius);

        foreach (var h in hits)
        {
            if (h.CompareTag("Enemy"))
                return true;
        }

        return false;
    }

    void Attack()
    {
        Vector2 c = transform.position;

        Hit(c);
        Hit(c + Vector2.up * tileSize);
        Hit(c + Vector2.down * tileSize);
        Hit(c + Vector2.left * tileSize);
        Hit(c + Vector2.right * tileSize);

        SpawnEffect(c);
        SpawnEffect(c + Vector2.up * tileSize);
        SpawnEffect(c + Vector2.down * tileSize);
        SpawnEffect(c + Vector2.left * tileSize);
        SpawnEffect(c + Vector2.right * tileSize);
    }

    void Hit(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, radius);

        foreach (var h in hits)
        {
            if (!h.CompareTag("Enemy"))
                continue;

            EnemyHealth enemyHealth = h.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
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
        Vector2 c = transform.position;
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(c, radius);
        Gizmos.DrawWireSphere(c + Vector2.up * tileSize, radius);
        Gizmos.DrawWireSphere(c + Vector2.down * tileSize, radius);
        Gizmos.DrawWireSphere(c + Vector2.left * tileSize, radius);
        Gizmos.DrawWireSphere(c + Vector2.right * tileSize, radius);
    }
}
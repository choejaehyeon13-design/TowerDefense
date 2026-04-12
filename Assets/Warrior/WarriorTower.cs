using UnityEngine;

public class WarriorTower : MonoBehaviour
{
    public int damage = 3;
    public float cooldown = 1f;

    public float tileSize = 1f;
    public float radius = 0.3f;

    public GameObject hitEffectPrefab; // 전사 공격 이펙트

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
            if (h.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = h.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
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
}
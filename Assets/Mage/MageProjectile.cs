using UnityEngine;

public class MageProjectile : Projectile
{
    public float tileSize = 1f;
    public float radius = 0.3f;
    public GameObject hitEffectPrefab;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        Vector2 center = col.transform.position;

        Hit(center);
        Hit(center + Vector2.up * tileSize);
        Hit(center + Vector2.down * tileSize);
        Hit(center + Vector2.left * tileSize);
        Hit(center + Vector2.right * tileSize);

        SpawnEffect(center);
        SpawnEffect(center + Vector2.up * tileSize);
        SpawnEffect(center + Vector2.down * tileSize);
        SpawnEffect(center + Vector2.left * tileSize);
        SpawnEffect(center + Vector2.right * tileSize);

        Destroy(gameObject);
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
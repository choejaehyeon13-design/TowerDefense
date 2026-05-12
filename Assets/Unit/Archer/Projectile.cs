using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;

    protected Transform target;
    protected int damage;

    public virtual void SetTarget(Transform newTarget, int newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir =
            (target.position - transform.position).normalized;

        // 이동 방향 바라보기
        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angle - 90f);

        // 이동
        transform.position +=
            (Vector3)(dir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy"))
            return;

        EnemyHealth enemyHealth =
            col.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
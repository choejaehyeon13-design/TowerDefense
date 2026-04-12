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

        Vector2 dir = (target.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

 protected virtual void OnTriggerEnter2D(Collider2D col)
{
    if (!col.CompareTag("Enemy")) return;

    Debug.Log("화살이 적과 충돌함");

    EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
    if (enemyHealth != null)
    {
        enemyHealth.TakeDamage(damage);
    }
    else
    {
        Debug.Log("EnemyHealth를 찾지 못함");
    }

    Destroy(gameObject);
    }
}

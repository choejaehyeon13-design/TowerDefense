using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    public float range = 7f;
    public float cooldown = 1f; // 초당 1발
    public int damage = 1;

    public GameObject arrowPrefab;
    public Transform firePoint;

    float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;

        Transform target = FindTarget();

        if (target != null && timer <= 0f)
        {
            Shoot(target);
            timer = cooldown;
        }
    }

    Transform FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearest = null;
        float min = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d <= range && d < min)
            {
                min = d;
                nearest = e.transform;
            }
        }

        return nearest;
    }

    void Shoot(Transform target)
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Projectile projectile = arrow.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }
}
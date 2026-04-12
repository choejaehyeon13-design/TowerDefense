using UnityEngine;

public class MageTower : MonoBehaviour
{
    public float range = 7f;
    public float cooldown = 3.33f; // 초당 0.3발
    public int damage = 3;

    public GameObject magicPrefab;
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
        GameObject magic = Instantiate(magicPrefab, firePoint.position, Quaternion.identity);
        MageProjectile projectile = magic.GetComponent<MageProjectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }
}
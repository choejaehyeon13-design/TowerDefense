using UnityEngine;

public class MageTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float range = 7f;
    public float cooldown = 0.3f;
    public int damage = 3;

    private float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;

        Transform target = FindNearestEnemy();

        if (target != null && timer <= 0f)
        {
            Shoot(target);
            timer = cooldown;
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
        if (projectilePrefab == null) return;

        GameObject projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }
}
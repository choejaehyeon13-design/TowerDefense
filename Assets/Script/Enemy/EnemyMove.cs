using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2f;

    private Transform[] wayPoints;
    private int currentIndex = 0;

    public void SetWayPoints(Transform[] points)
    {
        wayPoints = points;
        currentIndex = 0;

        if (wayPoints != null && wayPoints.Length > 0)
        {
            transform.position = wayPoints[0].position;
        }
    }

    void Update()
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        Transform target = wayPoints[currentIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;

            if (currentIndex >= wayPoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}
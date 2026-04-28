using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("적 이동 속도")]
    public float speed = 2f;

    private Transform[] wayPoints;
    private int currentIndex = 0;

    public void SetWayPoints(Transform[] points)
    {
        wayPoints = points;
        currentIndex = 0;

        Debug.Log("웨이포인트 받음: " + wayPoints.Length);
    }

    private void Update()
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        if (currentIndex >= wayPoints.Length)
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
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("웨이포인트 목록")]
    [SerializeField] private Transform[] points;

    public Transform[] GetWayPoints()
    {
        return points;
    }

    private void Awake()
    {
        // 배열이 비어 있으면, 자신의 자식들을 자동으로 웨이포인트로 등록
        if (points == null || points.Length == 0)
        {
            points = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                points[i] = transform.GetChild(i);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // 자식 오브젝트 기준으로 선 연결
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currentPoint = transform.GetChild(i);

            if (currentPoint != null)
            {
                Gizmos.DrawSphere(currentPoint.position, 0.2f);

                if (i < transform.childCount - 1)
                {
                    Transform nextPoint = transform.GetChild(i + 1);
                    Gizmos.DrawLine(currentPoint.position, nextPoint.position);
                }
            }
        }
    }
}
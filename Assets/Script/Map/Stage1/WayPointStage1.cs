using UnityEngine;

public class WayPoint_Stage1 : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

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
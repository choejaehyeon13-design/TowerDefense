using System.Collections;
using UnityEngine;

public class DragonEffect : MonoBehaviour
{
    public float maxRadius = 3f;
    public float duration = 0.4f;
    public Color shockwaveColor = new Color(1f, 0.5f, 0f, 1f);

    private LineRenderer lr;

    void Awake()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.loop = true;
        lr.widthMultiplier = 0.1f;
        lr.positionCount = 60;
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = shockwaveColor;
        lr.endColor = shockwaveColor;
        lr.sortingOrder = 10;
    }

    void Start()
    {
        StartCoroutine(Animate());
    }

    void DrawCircle(float radius)
    {
        for (int i = 0; i < 60; i++)
        {
            float angle = i * Mathf.PI * 2f / 60;
            lr.SetPosition(i, new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0
            ));
        }
    }

    IEnumerator Animate()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float radius = Mathf.Lerp(0f, maxRadius, t);

            Color c = shockwaveColor;
            c.a = 1f - t;
            lr.startColor = c;
            lr.endColor = c;

            DrawCircle(radius);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
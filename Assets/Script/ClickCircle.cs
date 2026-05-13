using UnityEngine;

public class ClickCircle : MonoBehaviour
{
    public Sprite circleSprite;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            GameObject obj = new GameObject("Circle");
            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();

            sr.sprite = circleSprite;

            obj.transform.position = pos;
        }
    }
}
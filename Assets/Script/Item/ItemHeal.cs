using UnityEngine;
public class ItemDragon : MonoBehaviour
{
    public float DragonRange = 2.5f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;


            EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(pos, enemy.transform.position);

                if (dist <= DragonRange)
                {
                    EnemyHealth hp = enemy.GetComponent<EnemyHealth>();

                    if (hp != null)
                        hp.TakeDamage(9999);
                        Debug.Log("Dragon hit an enemy!");
                }
            }
        }
    }
}





}
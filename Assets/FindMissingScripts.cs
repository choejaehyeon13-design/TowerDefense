using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void Find()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        int count = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    count++;
                    Debug.Log("Missing Script found on: " + GetFullPath(go), go);
                }
            }
        }

        Debug.Log("총 Missing Script 개수: " + count);
    }

    static string GetFullPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform.parent;

        while (current != null)
        {
            path = current.name + "/" + path;
            current = current.parent;
        }

        return path;
    }
}
using UnityEngine;

public static class GameLog
{
    public static bool enableLog = true;

    public static void Info(string message)
    {
        if (!enableLog) return;

        Debug.Log($"[INFO] {message}");
    }

    public static void Warning(string message)
    {
        if (!enableLog) return;

        Debug.LogWarning($"[WARNING] {message}");
    }

    public static void Error(string message)
    {
        if (!enableLog) return;

        Debug.LogError($"[ERROR] {message}");
    }
}
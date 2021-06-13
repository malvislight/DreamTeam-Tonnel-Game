using System.IO;
using UnityEngine;

public class JsonSerializer : MonoBehaviour
{
    private LevelInfo _levelInfo;

    private static readonly string _path = $"{Application.streamingAssetsPath}/JSON/пример.json";

    public static LevelInfo GetLevelInfo()
    {
        string json = File.ReadAllText(_path);
        return JsonUtility.FromJson<LevelInfo>(json);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelsContainer", menuName = "SO/GameLevelsContainer")]
public class GameLevelsContainer : ScriptableObject
{
    public GameObject[] Levels;
    [Min(1)] public int LevelIndex = 1;
    public const string LevelIndexKey = "Level Index";
    
    [ContextMenu("Set level index")]
    void SetLevelIndex()
    {
        PlayerPrefs.SetInt(LevelIndexKey, LevelIndex);
    }
}

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public const string TutorialKey = "tutorial showed";
    
    [SerializeField] GameObject _tutorialLevel;
    [SerializeField] GameLevelsContainer _levelsContainer;

    void Awake()
    {
        PlayerPrefs.SetInt(TutorialKey, 1);
        if (PlayerPrefs.GetInt(TutorialKey, 0) == 0)
        {
            Instantiate(_tutorialLevel,
                new Vector3(0, 0, 0),
                quaternion.identity,
                transform);
        }
        else
        {
            _levelsContainer.LevelIndex = PlayerPrefs.GetInt(GameLevelsContainer.LevelIndexKey, 1);
        
            InstantiateLevel(_levelsContainer.LevelIndex);
        }
    }

    void InstantiateLevel(int i)
    {
        i--; // because we start index from 1 to be correct on UI.
        i %= _levelsContainer.Levels.Length;

        Instantiate(_levelsContainer.Levels[i],
            new Vector3(0, 0, 0),
            quaternion.identity,
            transform);
    }

    public void LoadNextLevel()
    {
        _levelsContainer.LevelIndex++;
        PlayerPrefs.SetInt(GameLevelsContainer.LevelIndexKey, _levelsContainer.LevelIndex);
        
        SceneManager.LoadScene("Game");
    }
}

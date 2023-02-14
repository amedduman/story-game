using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void NextLevelButtonPressed()
    {
        ServiceLocator.Get<LevelManager>().LoadNextLevel();
    }

    public void OnRoadComplete()
    {
        ServiceLocator.Get<UIController>().HandleRoadComplete();
        ServiceLocator.Get<Girl>().HandleRoadCompletion();
    }
}
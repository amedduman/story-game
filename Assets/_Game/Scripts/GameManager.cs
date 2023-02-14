using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void NextLevelButtonPressed()
    {
        ServiceLocator.Get<LevelManager>().LoadNextLevel();
    }

    public void OnRoadComplete()
    {
        ServiceLocator.Get<Girl>().HandleRoadCompletion();
    }

    public void OnGirlReachedHome()
    {
        ServiceLocator.Get<UIController>().HandleGirlReachedHome();
    }
}
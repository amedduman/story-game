using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _startCanvas;
    
    public void HandleRoadComplete()
    {
        _startCanvas.SetActive(true);        
    }
    
    public void OnClick_NextButtonAction()
    {
        ServiceLocator.Get<GameManager>().NextLevelButtonPressed();
    }
}
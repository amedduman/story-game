using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _startCanvas;
    
    public void HandleGirlReachedHome()
    {
        _startCanvas.SetActive(true);        
    }
    
    public void OnClick_NextButtonAction()
    {
        ServiceLocator.Get<GameManager>().NextLevelButtonPressed();
    }
}
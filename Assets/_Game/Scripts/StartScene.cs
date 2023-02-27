using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnClick_Scan()
    {
        SceneManager.LoadScene("_Game/Scenes/QrScannerScene");
    }
}

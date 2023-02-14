using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;

public class QRScanner : MonoBehaviour
{
    [SerializeField] RawImage _rawImageToDisplayCamera;
    [SerializeField] AspectRatioFitter _aspectRatioFitter;
    [SerializeField] Button _playBtn;
    [SerializeField] RectTransform _scanZone;

    bool _isCamAvailable;
    WebCamTexture _camTexture;

    IEnumerator Start()
    {
        SetUpCamera();
        while (true)
        {
            yield return new WaitForSecondsRealtime(.5f);
            Scan();
        }
    }

    void Update()
    {
        UpdateCameraRenderer();
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Scan();
        }
#endif
    }

    void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("no cam");
            _isCamAvailable = false;
            return;
        }
        
        foreach (var device in devices)
        {
#if UNITY_EDITOR
            _camTexture = new WebCamTexture(device.name,
                (int)_scanZone.rect.width,
                (int)_scanZone.rect.width);

#endif
            if (!device.isFrontFacing)
            {
                _camTexture = new WebCamTexture(device.name,
                    (int)_scanZone.rect.width,
                    (int)_scanZone.rect.width);
            }
        }
        
        _camTexture.Play();
        _rawImageToDisplayCamera.texture = _camTexture;
        _isCamAvailable = true;
    }

    void UpdateCameraRenderer()
    {
        if (!_isCamAvailable) return;
        float ratio = (float)_camTexture.width / (float)_camTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_camTexture.videoRotationAngle;  
        _rawImageToDisplayCamera.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    void Scan()
    {
        try
        {
            IBarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
            if (result != null)
            {
                // _outputText.text = result.Text;
                _playBtn.gameObject.SetActive(true);
            }
            else
            {
                // _outputText.text = "failed to read qr code";
                Debug.Log("failed to read qr code");
            }
        }
        catch
        {
            // _outputText.text = "fail to scan";
            Debug.Log("fail to scan");
        }
    }

    public void OnClick_LoadGameScene()
    {
        SceneManager.LoadScene("_Game/Scenes/Game");
    }
}
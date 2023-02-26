using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform _hand;
    [SerializeField] Transform _endPoint;
    
    
    
    
    string _tutorialShowedKey = "Tutorial Showed";

    void Start()
    {
        if (PlayerPrefs.GetInt(_tutorialShowedKey, 0) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _hand.DOMove(_endPoint.position, 1).SetLoops(-1, LoopType.Restart);
        }
        
        PlayerPrefs.SetInt(_tutorialShowedKey, 1);

    }

    void OnDestroy()
    {
        DOTween.Kill(_hand);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}

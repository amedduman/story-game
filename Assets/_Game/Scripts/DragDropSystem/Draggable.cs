using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] Collider _col;
    Vector3 _startingPos;

    void Awake()
    {
        _startingPos = transform.position;
    }

    public void StartDrag()
    {
        _col.enabled = false;
    }

    public void Drag(RaycastHit hitInfo)
    {
        // transform.position = hitInfo.transform.position;
        transform.DOMove(hitInfo.transform.position, .8f);
    }

    public void StopDrag()
    {
        _col.enabled = true;
        ReturnRestingPos();
    }

    public void ReturnRestingPos()
    {
        transform.DOMove(_startingPos, .5f);
    }
}

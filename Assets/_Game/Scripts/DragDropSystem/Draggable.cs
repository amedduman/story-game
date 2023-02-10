using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] GameObject _colParent;
    public event Action OnDrop;

    public void StartDrag()
    {
        _colParent.SetActive(false);
    }

    public void Drag(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Tile"))
        {
            transform.DOMove(hitInfo.transform.position, .1f);
        }
        else if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            transform.DOMove(hitInfo.point, .1f);
        }
    }

    public void Drop()
    {
        OnDrop?.Invoke();
    }

    public void EnableDraggable()
    {
        _colParent.SetActive(true);
    }
}

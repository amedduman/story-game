using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public event Action OnDrop;
    [SerializeField] Collider _col;


    public void StartDrag()
    {
        _col.enabled = false;
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
        _col.enabled = true;
    }
}

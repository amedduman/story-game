using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadPiece : MonoBehaviour
{
    [SerializeField] Draggable _draggable;
    [SerializeField] Raycaster[] _raycastPoints;
    
    Vector3 _startingPos;

    void Awake()
    {
        _startingPos = transform.position;
    }
    
    void OnEnable()
    {
        _draggable.OnDrop += HandleDrop;
    }

    void OnDisable()
    {
        _draggable.OnDrop += HandleDrop;
    }

    void HandleDrop()
    {
        List<Tile> tiles = new List<Tile>();
        foreach (var raycastPoint in _raycastPoints)
        {
            RaycastHit? hitInfo = raycastPoint.Raycast();
            if (hitInfo == null) continue;
            RaycastHit valueHitInfo = (RaycastHit)hitInfo;
            Tile tile = valueHitInfo.transform.GetComponentInParent<Tile>();
            if (tile != null)
            {
                tiles.Add(tile);
            }
            else
            {
                ReturnRestingPos();
                break;
            }
        }

        _draggable.EnableDraggable();

    }
    
    void ReturnRestingPos()
    {
        transform.DOMove(_startingPos, .5f).OnComplete(() => _draggable.EnableDraggable());
    }
}
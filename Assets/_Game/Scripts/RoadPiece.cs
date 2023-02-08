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
        List<Tile> _tiles = new List<Tile>();
        for (int i = 0; i < _raycastPoints.Length; i++)
        {
            RaycastHit? hitInfo = _raycastPoints[i].Raycast();
            if (hitInfo == null) continue;
            RaycastHit valueHitInfo = (RaycastHit)hitInfo;
            Tile tile = valueHitInfo.transform.GetComponentInParent<Tile>();
            if (tile != null)
            {
                _tiles.Add(tile);
            }
            else
            {
                ReturnRestingPos();
                break;
            }
        }
    }
    
    void ReturnRestingPos()
    {
        transform.DOMove(_startingPos, .5f);
    }
}
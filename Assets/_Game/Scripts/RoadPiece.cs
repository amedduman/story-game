using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class RoadPiece : MonoBehaviour
{
    [SerializeField] Transform _raycastPointsParent;
    // [SerializeField] Raycaster[] _raycastPoints;
    
    Draggable _draggable;
    Vector3 _startingPos;

    void Awake()
    {
        _draggable = GetComponent<Draggable>();
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
        for (int i = 0; i < _raycastPointsParent.childCount; i++)
        {
            Raycaster raycastPoint = _raycastPointsParent.GetChild(i).GetComponent<Raycaster>();
            
            RaycastHit? hitInfo = raycastPoint.Raycast();
            if (hitInfo == null)
            {
                ReturnRestingPos();
                return;
            }
            RaycastHit valueHitInfo = (RaycastHit)hitInfo;
            Tile tile = valueHitInfo.transform.GetComponentInParent<Tile>();
            if (tile != null)
            {
                tiles.Add(tile);
            }
            else
            {
                ReturnRestingPos();
                // break;
                return;
            }
        }
        // foreach (var raycastPoint in _raycastPoints)
        // {
        //     RaycastHit? hitInfo = raycastPoint.Raycast();
        //     if (hitInfo == null)
        //     {
        //         ReturnRestingPos();
        //         return;
        //     }
        //     RaycastHit valueHitInfo = (RaycastHit)hitInfo;
        //     Tile tile = valueHitInfo.transform.GetComponentInParent<Tile>();
        //     if (tile != null)
        //     {
        //         tiles.Add(tile);
        //     }
        //     else
        //     {
        //         ReturnRestingPos();
        //         // break;
        //         return;
        //     }
        // }

        _draggable.EnableDraggable();

    }
    
    void ReturnRestingPos()
    {
        transform.DOMove(_startingPos, .5f).OnComplete(() => _draggable.EnableDraggable());
    }
}
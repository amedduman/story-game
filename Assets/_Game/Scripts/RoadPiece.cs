using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class RoadPiece : MonoBehaviour
{
    public List<Tile> Tiles {get; private set;} = new List<Tile>();
    
    [SerializeField] Transform _raycastPointsParent;
    
    Draggable _draggable;
    Vector3 _startingPos;

    void Awake()
    {
        _draggable = GetComponent<Draggable>();
        _startingPos = transform.position;
    }

    void Start()
    {
        ServiceLocator.Get<RoadCompletionChecker>().AddRoad(this);
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
                Tiles.Add(tile);
            }
            else
            {
                ReturnRestingPos();
                return;
            }
        }

        ServiceLocator.Get<RoadCompletionChecker>().CheckRoad(Tiles);
        _draggable.EnableDraggable();

    }
    
    void ReturnRestingPos()
    {
        Tiles.Clear();
        transform.DOMove(_startingPos, .5f).OnComplete(() => _draggable.EnableDraggable());
    }
}
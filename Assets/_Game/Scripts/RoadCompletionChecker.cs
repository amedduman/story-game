using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadCompletionChecker : MonoBehaviour
{
    [SerializeField] Tile _startTile;
    [SerializeField] Tile _endTile;

    List<RoadPiece> _roads = new List<RoadPiece>();
    List<RoadPiece> _sortedRoads = new List<RoadPiece>();
    RoadPiece _firstRoad;
    RoadPiece _lastRoad;
    RoadConnectionPoint _firstRcp;
    
    void Start()
    {
        _startTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        _endTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
    }

    public void AddRoad(RoadPiece rp)
    {
        _roads.Add(rp);        
    }

    public void CheckRoad()
    {
        _sortedRoads.Clear();
        _firstRoad = null;
        _lastRoad = null;
        
        foreach (var road in _roads)
        {
            if (road.IsFirstRoad())
            {
                foreach (var rcp in road._roadConnectionPoints)
                {
                    if (rcp.IsStartPoint())
                    {
                        _firstRcp = rcp;
                    }
                }
                _sortedRoads.Add(road);
                _firstRoad = road;
                break;
            }
        }

        if (_firstRoad == null)
        {
            Debug.Log("first road is empty");

            return;
        }

        foreach (var road in _roads)
        {
            if (road.IsLastRoad())
            {
                _lastRoad = road;
                break;
            }
        }

        if (_lastRoad == null)
        {
            Debug.Log("last road is empty");
            return;
        }
        
        _sortedRoads.Add(_firstRoad);
        StartCoroutine(AddConnectedRoads());

        foreach (var sortedRoad in _sortedRoads)
        {
            Debug.Log(sortedRoad.gameObject.name, sortedRoad.gameObject);
        }
    }
    
    IEnumerator AddConnectedRoads()
    {
        RoadConnectionPoint rcp = _firstRcp;
        AddRoad(rcp);

        yield return new WaitForFixedUpdate();
        
        while (true)
        {
            Debug.Log("searching");
            rcp = rcp.GetPeerPoint();
            
            if (rcp.GetConnectionPoint() != null)
            {
                rcp = rcp.GetConnectionPoint();
                AddRoad(rcp);
            }
            else
            {
                if (rcp.IsEndPoint())
                {
                    Debug.Log("complete");
                    yield break;
                }
                else
                {
                    Debug.Log("not complete");
                    yield break;    
                }
            }
            yield return null;
        }
    }

    void AddRoad(RoadConnectionPoint rcp)
    {
        if (_sortedRoads.Contains(rcp.GetRoad()) == false)
        {
            _sortedRoads.Add(rcp.GetRoad());
        }
    }
}
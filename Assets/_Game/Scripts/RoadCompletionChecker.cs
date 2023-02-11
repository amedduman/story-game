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
        Debug.Log("start checking");
        StartCoroutine(AddConnectedRoads());

        foreach (var sortedRoad in _sortedRoads)
        {
            Debug.Log(sortedRoad.gameObject.name, sortedRoad.gameObject);
        }
    }

    IEnumerator AddConnectedRoads()
    {
        while (true)
        {
            Debug.Log("searching");
            List<RoadPiece> roads = _sortedRoads[^1].GetConnectedRoads();
            foreach (var road in roads)
            {
                if (road != null)
                {
                    if (_sortedRoads.Contains(road) == false)
                    {
                        _sortedRoads.Add(road);
                    }

                    if (road.IsLastRoad())
                    {
                        Debug.Log("road Complete");
                        yield break;
                    }
                }
            }
            yield return null;
        }
        // _sortedRoads.Add(_lastRoad);
    }
    
    
}
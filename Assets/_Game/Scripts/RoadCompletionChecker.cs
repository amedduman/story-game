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
    List<Tile> _roadTiles = new List<Tile>();

    void Start()
    {
        _startTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        _endTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
    }

    public void AddRoad(RoadPiece rp)
    {
        _roads.Add(rp);        
    }

    public void CheckRoad(List<Tile> tiles)
    {
        _roadTiles.Clear();
        
        foreach (var road in _roads)
        {
            _roadTiles.AddRange(road.Tiles);
        }
        _roadTiles.Add(_endTile);
        
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        Tile currenTile = _startTile;

        while (currenTile != _endTile)
        {
            List<Tile> currentNeighbours = currenTile.GetNeighbours();

            foreach (var currentNeighbour in currentNeighbours)
            {
                currentNeighbour.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        
            bool roadAvailable = false;
        
            foreach (var tile in _roadTiles)
            {
                if (currentNeighbours.Contains(tile))
                {
                    roadAvailable = true;
                    currenTile = tile;
                    if (currenTile == _endTile)
                    {
                        Debug.Log("complete");
                        yield break;
                    }
                    _roadTiles.Remove(currenTile);
                    currenTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    break;
                } 
            }
            
            if (roadAvailable)
            {
                Debug.Log("searching road");
                Debug.Break();
            }
            else
            {
                Debug.Log("not complete");
                yield break;
            }
            yield return null;
        }
    }
}
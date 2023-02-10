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
    List<Tile> _allTiles = new List<Tile>();

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
        _allTiles.Clear();
        
        foreach (var road in _roads)
        {
            _allTiles.AddRange(road.Tiles);
        }
        
        foreach (var allTile in _allTiles)
        {
            allTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        }

        // StartCoroutine(Check());
        // Check(_startTile);
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
        
            foreach (var tile in _allTiles)
            {
                if (currentNeighbours.Contains(tile))
                {
                    roadAvailable = true;
                    currenTile = tile;
                    currenTile.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    break;
                } 
            }
            
            if (roadAvailable)
            {
                Debug.Log("searching road");
            }
            else
            {
                Debug.Log("not complete");
            }
            Debug.Break();
            yield return null;
        }
    }

    // int i;
    // void Check(Tile ttile)
    // {
    //     i++;
    //     if (i > 999)
    //     {
    //         Debug.Log("reached limit");
    //         return;
    //     }
    //     Tile currenTile = ttile;
    //
    //     if (currenTile == _endTile)
    //     {
    //         Debug.Log("complete");
    //         return;
    //     }
    //     List<Tile> currentNeighbours = currenTile.GetNeighbours();
    //
    //     foreach (var currentNeighbour in currentNeighbours)
    //     {
    //         currentNeighbour.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    //     }
    //     
    //     foreach (var currentNeighbour in currentNeighbours)
    //     {
    //         currentNeighbour.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
    //     }
    //     
    //     bool roadAvailable = false;
    //     Tile newTile = null;
    //     
    //     foreach (var tile in _allTiles)
    //     {
    //         if (currentNeighbours.Contains(tile))
    //         {
    //             roadAvailable = true;
    //             newTile = tile;
    //             break;
    //         } 
    //     }
    //
    //     if (roadAvailable)
    //     {
    //         Debug.Log("searching road");
    //         Check(newTile);
    //     }
    //     else
    //     {
    //         Debug.Log("not complete");
    //     }
    // }
}
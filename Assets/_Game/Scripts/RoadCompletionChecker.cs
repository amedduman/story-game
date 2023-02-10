using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadCompletionChecker : MonoBehaviour
{
    [SerializeField] Tile _startTile;
    [SerializeField] Tile _endTile;

    List<RoadPiece> _roads = new List<RoadPiece>();
    List<Tile> _allTiles = new List<Tile>();


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

        Check(_startTile);
    }

    void Check(Tile ttile)
    {
        if (ttile == _endTile)
        {
            Debug.Log("complete");
            
        }
        Tile currenTile = ttile;
        List<Tile> currentNeighbours = currenTile.GetNeighbours();
        foreach (var tile in _allTiles)
        {
            if (currentNeighbours.Contains(tile)) DOVirtual.DelayedCall(.1f, () => Check(tile));
        }

        Debug.Log("not complete");
    }
}
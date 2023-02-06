using System;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public int tileIdX { get; set; }
    public int tileIdZ { get; set; }
    
    public Tile _nTileForward;
    public Tile _nTileBackward;
    public Tile _nTileLeft;
    public Tile _nTileRight;
    
    [SerializeField] LayerMask _tileLayer;
    
    public void SetNeighbors()
    {
        Vector3 origin = transform.position;
        Vector3 size = GetComponentInChildren<Renderer>().bounds.size;
        origin.y += size.y / 2;
        
        Ray rayForward = new Ray(origin, Vector3.forward * size.z);
        NeighbourTileRaycast(rayForward, size, ref _nTileForward);
        
        Ray rayBackward = new Ray(origin, Vector3.back * size.z);
        NeighbourTileRaycast(rayBackward, size, ref _nTileBackward);
        
        Ray rayLeft = new Ray(origin, Vector3.left * size.x);
        NeighbourTileRaycast(rayLeft, size, ref _nTileLeft);
        
        Ray rayRight = new Ray(origin, Vector3.right * size.x);
        NeighbourTileRaycast(rayRight, size, ref _nTileRight);
    }

    void NeighbourTileRaycast(Ray ray, Vector3 size, ref Tile nTile)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, size.z, _tileLayer))
        {
            Tile tile = hitInfo.transform.GetComponentInParent<Tile>();
            if (tile != null)
            {
                nTile = tile;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;
        Vector3 size = GetComponentInChildren<Renderer>().bounds.size;
        origin.y += size.y / 2;
        Ray rayForward = new Ray(origin, Vector3.forward * size.z);
        Ray rayBackward = new Ray(origin, Vector3.back * size.z);
        Ray rayLeft = new Ray(origin, Vector3.left * size.x);
        Ray rayRight = new Ray(origin, Vector3.right * size.x);
        
        Debug.DrawRay(rayForward.origin, rayForward.direction * size.z);
        Debug.DrawRay(rayBackward.origin, rayBackward.direction * size.z);
        Debug.DrawRay(rayLeft.origin, rayLeft.direction * size.x);
        Debug.DrawRay(rayRight.origin, rayRight.direction * size.x);
        Gizmos.DrawSphere(origin, .1f);
    }
}

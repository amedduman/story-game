using System;
using UnityEngine;
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
	Camera gameCam;
	[SerializeField] Tile tilePrefab;
	[SerializeField] float _tilesPosOnZAxis;
	[SerializeField] int xSize = 8;
	[SerializeField] int zSize = 8;
	
	Tile[,] tiles;
#if UNITY_EDITOR
	
	[ContextMenu("Generate Grid")]
	void GenerateGrid () 
	{
		DestroyGrid();
		tiles = new Tile[xSize, zSize];

		// calculate needed distance between tiles
		Vector3 offset = tilePrefab.GetComponentInChildren<Renderer>().bounds.size;
		
		// generate grid
		for (int i = 0; i < xSize; i++) 
		{
			for (int j = 0; j < zSize; j++) 
			{
				Tile tile = PrefabUtility.InstantiatePrefab(tilePrefab, transform) as Tile ;

                tile.transform.localPosition = new Vector3(offset.x * i, 0, offset.z * j);

				NameTile(tile, i, j);

				SetTileIds(tile, i, j);
				
				tiles[i, j] = tile; 
			}
        }
    }
#endif

	private void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Tile tile = transform.GetChild(i).GetComponent<Tile>();
			tile.SetNeighbors();
		}
	}

	void SetTileIds(Tile tile, int x, int z)
	{
		tile.tileIdX = x;
		tile.tileIdZ = z;
	}

	void NameTile(Tile tile, int x, int y)
	{
		tile.gameObject.name = $"Tile ({x + 1},{y + 1})";
	}

	[ContextMenu("Destroy Grid")]
    void DestroyGrid()
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}	 
	}
}

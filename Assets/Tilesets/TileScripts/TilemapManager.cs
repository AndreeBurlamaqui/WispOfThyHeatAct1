using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance { get; private set; }

    [SerializeField] private Tilemap[] map;

    [SerializeField] private List<TileTypeData> _tileDatas;

    private Dictionary<TileBase, TileTypeData> dataFromTiles = new Dictionary<TileBase, TileTypeData>();

    private void Awake()
    {
        Instance = this;

        UpdateTilemapArray();

        //map = GetComponent<Tilemap>();

        foreach (TileTypeData td in _tileDatas)
        {
            foreach (Tile t in td.tiles)
            {
                dataFromTiles.Add(t, td);
            }
        }
    }

    public void UpdateTilemapArray()
    {
        Tilemap[] newMapArray = FindObjectsOfType<Tilemap>();
        map = new Tilemap[newMapArray.Length];

        map = newMapArray;

    }

    public TypeFloor GetTypeFloor(Vector2 worldPosition)
    {
        foreach (Tilemap tmap in map)
        {
            if (tmap != null)
            {
                Vector3Int gridPosition = tmap.WorldToCell(worldPosition);

                if (tmap.HasTile(gridPosition))
                {
                    TileBase tile = tmap.GetTile(gridPosition);

                    if (tile == null)
                        return TypeFloor.Default;

                    if (dataFromTiles[tile]._typeFloor != 0)
                    {
                        TypeFloor tf = dataFromTiles[tile]._typeFloor;

                        return tf;
                    }
                }
                else
                {
                    return TypeFloor.Default;
                }
            }
        }
        return TypeFloor.Default;
    }

}

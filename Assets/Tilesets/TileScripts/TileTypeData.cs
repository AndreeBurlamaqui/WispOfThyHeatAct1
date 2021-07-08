using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Custom Tile", menuName = "TinyCacto/Custom Tile")]
public class TileTypeData : ScriptableObject
{
    public TileBase[] tiles;
   public TypeFloor _typeFloor;
}

public enum TypeFloor { Default, Grass, Wood, Stone, Metal }

using System;
using UnityEngine;

public static class TileUtils
{
    public static Color TileTypeToColor(TileType type)
    {
        switch (type)
        {
            case TileType.None:
                return Color.black;
            case TileType.Empty:
                return Color.clear;
            case TileType.Red:
                return Color.red;
            case TileType.Green:
                return Color.green;
            case TileType.Blue:
                return Color.blue;
            case TileType.Yellow:
                return Color.yellow;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

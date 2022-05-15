using System;
using UnityEngine;

public class Match3Tile
{
    protected TileType type;
    private Vector2Int position;
    private bool _searched;

    public Vector2Int Position => position;

    public Match3Tile(Vector2Int pos)
    {
        position = pos;
        _searched = false;
        type = TileType.None;
    }
    public bool Searched
    {
        get => _searched;
        set => _searched = value;
    }

    public TileType Type
    {
        get => type;
        set => type = value;
    }

    public override string ToString()
    {
        switch (Type)
        {
            case TileType.None:
                return " ";
            case TileType.Empty:
                return "*";
            case TileType.Red:
                return "R";
            case TileType.Blue:
                return "B";
            case TileType.Green:
                return "G";
            case TileType.Yellow:
                return "Y";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

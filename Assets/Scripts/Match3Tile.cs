using System;
using UnityEngine;

public class Match3Tile
{
    protected TileType type;
    private Vector2Int position;
    private bool _searched;
    private int _dropAmount;
    private bool hasDropped;
    public bool HasDropped
    {
        get => hasDropped;
        set => hasDropped = value;
    }

    public Vector2Int Position => position;
    public int DropAmount
    {
        get => _dropAmount;
        set => _dropAmount = value;
    }

    public Match3Tile(Vector2Int pos)
    {
        position = pos;
        _searched = false;
        type = TileType.None;
        _dropAmount = 0;
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

    public void DroppedBy(int dropAmount)
    {
        if (dropAmount > 0)
        {
            hasDropped = true;
        }
        _dropAmount = dropAmount;
        position.x -= dropAmount;
    }
}

using System;
using UnityEngine;

public class Match3Tile 
{
    protected TileType type;
    private Vector2Int _position;
    private bool _searched;
    private int _dropAmount;
    private bool _hasDropped;
    
    
    public static bool operator == (Match3Tile a, Match3Tile b){
        if (ReferenceEquals(null, a)) return false;
        if (ReferenceEquals(null, b)) return false;
        if (ReferenceEquals(a, b)) return true;
        return a.type == b.type;
    }

    public static bool operator !=(Match3Tile a, Match3Tile b)
    {
        return !(a == b);
    }

    #region Getters and Setters
    public Vector2Int Position
    {
        get => _position;
        set => _position = value;
    }

    public bool HasDropped
    {
        get => _hasDropped;
        set => _hasDropped = value;
    }

    public int DropAmount
    {
        get => _dropAmount;
        set => _dropAmount = value;
    }

    public Match3Tile(Vector2Int pos)
    {
        _position = pos;
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
    #endregion

    public void DroppedBy(int dropAmount)
    {
        if (dropAmount > 0)
        {
            _hasDropped = true;
        }
        _dropAmount = dropAmount;
        _position.x -= dropAmount;
    }

    public bool IsValid()
    {
        return Type == TileType.None || Type == TileType.Empty;
    }


}

using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Match3Board
{
    private readonly int _size;
    private readonly Match3Tile[,] _tiles;
    public Match3Tile[,] Tiles => _tiles;
    public Match3Board(int size, TileType[] availableTileTypes)
    {
        _size = size;
        _tiles = new Match3Tile[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _tiles[i, j] = new Match3Tile(new Vector2Int(i, j));
            }   
        }
        SetRandomTileTypeForAllTiles(availableTileTypes);
    }

    private void SetRandomTileTypeForAllTiles(TileType[] availableTileTypes)
    {
        foreach (var tile in _tiles)
        {
            tile.Type = availableTileTypes[Random.Range(0, availableTileTypes.Length)];
        }
    }

    public void ClickedOnTile(int row, int col)
    {
        if (_tiles[row,col].Type == TileType.None ||
            _tiles[row, col].Type == TileType.Empty) return;
        
        //We need at least 2 connected tiles, so no connected neighbors means
        //no need to check for anything else
        if(!HasTileAnyMatchingNeighbor(row, col)) return;
        
        var tileListToDestroy = GetTilesToDestroy(row, col);
        if(tileListToDestroy.Count < 2) return;
        DestroyTiles(tileListToDestroy);
    }

    private void DestroyTiles(List<Vector2Int> tileListToDestroy)
    {
        for (int i = 0; i < tileListToDestroy.Count; i++)
        {
            var tilePos = tileListToDestroy[i];
            _tiles[tilePos.x, tilePos.y].Type = TileType.Empty;
        }
        Match3Actions.TilesDestroyed(tileListToDestroy);
    }

    public bool HasTileAnyMatchingNeighbor(int row, int col)
    {
        if (IsLeftNeighborTheSameType(row, col)) return true;
        if (IsRightNeighborTheSameType(row, col)) return true;
        if (IsBottomNeighborTheSameType(row, col)) return true;
        if (IsTopNeighborTheSameType(row, col)) return true;
        return false;
    }

    private bool IsLeftNeighborTheSameType(int row, int col)
    {
        return col > 0 && _tiles[row, col].Type == _tiles[row, col -1].Type;
    }

    private bool IsRightNeighborTheSameType(int row, int col)
    {
        return col < _size - 1 && _tiles[row, col].Type == _tiles[row , col + 1].Type;
    }
    private bool IsTopNeighborTheSameType(int row, int col)
    {
        return row < _size - 1 && _tiles[row, col].Type == _tiles[row + 1, col].Type;
    }

    private bool IsBottomNeighborTheSameType(int row, int col)
    {
        return row > 0 && _tiles[row, col].Type == _tiles[row - 1, col].Type;
    }

    public List<Vector2Int> GetTilesToDestroy(int row, int col)
    {
        List<Vector2Int> tilePositions = new List<Vector2Int>();
        Queue<Match3Tile> tilesToSearch = new Queue<Match3Tile>();
        
        ResetAllTilesSearchStatus();
        
        tilesToSearch.Enqueue(_tiles[row,col]);
        while (tilesToSearch.Count > 0)
        {
            var currentTile = tilesToSearch.Dequeue();
            tilePositions.Add(currentTile.Position);
            currentTile.Searched = true;
            
            int r = currentTile.Position.x;
            int c = currentTile.Position.y;
            
            //Add neighbors if !searched and valid
            if (IsLeftNeighborTheSameType(r, c) && !_tiles[r, c - 1].Searched)
            {
                var neighbor = _tiles[r, c - 1];
                tilesToSearch.Enqueue(neighbor);
                tilePositions.Add(neighbor.Position);
                neighbor.Searched = true;
            }
            
            if (IsRightNeighborTheSameType(r, c) && !_tiles[r, c + 1].Searched)
            {
                var neighbor = _tiles[r, c + 1];
                tilesToSearch.Enqueue(neighbor);
                tilePositions.Add(neighbor.Position);
                neighbor.Searched = true;
            }
            
            if (IsTopNeighborTheSameType(r, c) && !_tiles[r + 1, c].Searched)
            {
                var neighbor = _tiles[r + 1, c];
                tilesToSearch.Enqueue(neighbor);
                tilePositions.Add(neighbor.Position);
                neighbor.Searched = true;
            }
            
            if (IsBottomNeighborTheSameType(r, c) && !_tiles[r - 1, c].Searched)
            {
                var neighbor = _tiles[r - 1, c];
                tilesToSearch.Enqueue(neighbor);
                tilePositions.Add(neighbor.Position);
                neighbor.Searched = true;
            }
        }

        return tilePositions;
    }

    private void ResetAllTilesSearchStatus()
    {
        foreach (var tile in _tiles)
        {
            tile.Searched = false;
        }
    }

    public override string ToString()
    {
        if (_tiles == null || _size == 0) return "";
        
        StringBuilder builder = new StringBuilder();
        
        for (int i = _size - 1; i >= 0 ; i--)
        {
            for (int j = 0; j < _size; j++)
            {
                builder.Append(_tiles[i, j]);
            }

            builder.Append("\n");
        }
        return builder.ToString();
    }
}

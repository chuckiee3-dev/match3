using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{
    
    [Range(5,9)]
    [SerializeField] private int boardSize = 5;

    [SerializeField] private TileType[] tileTypes = { TileType.Red , TileType.Green, TileType.Blue, TileType.Yellow};
    
    private Match3Board _board;

    [SerializeField] private TileVisual tilePrefab;
    private TileVisual[,] _tileVisuals;
    private void Awake()
    {
        _board = new Match3Board(boardSize, tileTypes);
        _tileVisuals = new TileVisual[boardSize, boardSize];

        GenerateVisuals();
        Debug.Log(_board);
    }

    private void GenerateVisuals()
    {
        Vector3 startPosition = Vector3.right * boardSize + Vector3.up * boardSize;
        startPosition /= -2;
        
        for (int i = 0; i < boardSize ; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                _tileVisuals[i, j] = Instantiate(tilePrefab,startPosition+ Vector3.right *j + Vector3.up* i, quaternion.identity);
                _tileVisuals[i,j].SetColor(TileTypeToColor(_board.Tiles[i,j].Type));
                _tileVisuals[i,j].SetPos(new Vector2Int(i,j));
            }

        }
    }

    private Color TileTypeToColor(TileType type)
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

    private void OnEnable()
    {
        Match3Actions.OnClickTile += ClickedTileAtPosition;
        Match3Actions.OnTilesDestroyed += DestroyTiles;
    }
    private void OnDisable()
    {
        Match3Actions.OnClickTile -= ClickedTileAtPosition;
        Match3Actions.OnTilesDestroyed -= DestroyTiles;
    }
    
    private void DestroyTiles(List<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            _tileVisuals[pos.x, pos.y].GetDestroyed();
        }
    }

    private void ClickedTileAtPosition(Vector2Int pos)
    {
        _board.ClickedOnTile(pos.x, pos.y);
        Debug.Log("After click");
        Debug.Log(_board);
    }
}

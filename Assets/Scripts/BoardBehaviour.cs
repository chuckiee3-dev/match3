using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

public class BoardBehaviour : MonoBehaviour
{
    [SerializeField]private GameSettings settings;
    private Match3Board _board;
    private TileVisual[,] _tileVisuals;
    private GameInteractionCanvas _gameInteractionCanvas;
    private void Awake()
    {
        Assert.IsNotNull(settings, "You should assign Settings variable to create a board.");
        _board = new Match3Board(settings.size, settings.tileTypes);
        _tileVisuals = new TileVisual[settings.size, settings.size];
    
        GenerateVisuals();
        SetupBlocker();
    }

    private void SetupBlocker()
    {
        _gameInteractionCanvas = Instantiate(settings.gameInteractionCanvasPrefab);
        _gameInteractionCanvas.SetBlockDuration(settings.animationDuration);
    }

    private void GenerateVisuals()
    {
        Vector3 startPosition = Vector3.right * settings.size + Vector3.up * settings.size;
        startPosition /= -2;
        for (int i = 0; i < settings.size ; i++)
        {
            for (int j = 0; j < settings.size; j++)
            {
                SetupTileAtPosition(i, j, startPosition+ Vector3.right *j + Vector3.up* i);
            }

        }
    }

    private void SetupTileAtPosition(int i, int j, Vector3 pos)
    {
        _tileVisuals[i, j] = Instantiate(settings.tilePrefab, pos, quaternion.identity);
        _tileVisuals[i, j].transform.SetParent(transform);
        _tileVisuals[i,j].SetSettings(settings);
        _tileVisuals[i,j].SetColor(TileTypeToColor(_board.Tiles[i,j].Type));
        _tileVisuals[i,j].SetPos(new Vector2Int(i,j));
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

    private void MoveExistingDroppedTiles()
    {
        foreach (var tile in _board.Tiles)
        {
            //i row j column
            if (tile.HasDropped)
            {
                
                int row = tile.Position.x;
                int col = tile.Position.y;
                var tmp = _tileVisuals[row + tile.DropAmount, col];
                 tmp.DropBy(tile.DropAmount);
                 _tileVisuals[row + tile.DropAmount, col] = _tileVisuals[row, col];
                _tileVisuals[row , col] = tmp;
                _board.ResetDropFlag(row, col);
            }
        }
    }
    
    private void MoveNewDroppedTiles(List<Vector2Int> positionsToFill)
    {
        Vector3 startPosition = Vector3.right * settings.size + Vector3.up * settings.size;
        startPosition /= -2;
        for (int i = 0; i < settings.size ; i++)
        {
            for (int j = 0; j < settings.size; j++)
            {
                if(_board.Tiles[i,j].HasDropped)
                {
                    var posToDrop = startPosition + Vector3.right * j + Vector3.up * i;
                    
                    SetupTileAtPosition(i,j, posToDrop + Vector3.up * _board.Tiles[i, j].DropAmount);
                    
                    _tileVisuals[i,j].AnimateDropBy(_board.Tiles[i, j].DropAmount);
                    _board.ResetDropFlag(i, j);
                }
            }

        }
    }
    private void OnEnable()
    {
        Match3Actions.onClickTile += ClickedTileAtPosition;
        Match3Actions.onTilesDestroyed += DestroyTiles;
        Match3Actions.onExistingTilesDropped += MoveExistingDroppedTiles;
        Match3Actions.onTilesFilledFromAbove += MoveNewDroppedTiles;
    }



    private void OnDisable()
    {
        Match3Actions.onClickTile -= ClickedTileAtPosition;
        Match3Actions.onTilesDestroyed -= DestroyTiles;
        Match3Actions.onExistingTilesDropped -= MoveExistingDroppedTiles;
        Match3Actions.onTilesFilledFromAbove += MoveNewDroppedTiles;
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
    }
}

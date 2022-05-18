using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

public class BoardBehaviour : MonoBehaviour
{
    private GameSettings _settings;
    private Match3Board _board;
    private TileVisual[,] _tileVisuals;
    private GameInteractionCanvas _gameInteractionCanvas;

    public void Setup(GameSettings settings)
    {
        _settings = settings;
        Assert.IsNotNull(settings, "You should assign Settings variable to create a board.");
        _board = new Match3Board(settings.size, settings.tileTypes);
        _tileVisuals = new TileVisual[settings.size, settings.size];
    
        GenerateVisuals();
        SetupBlocker();
    }
    private void SetupBlocker()
    {
        _gameInteractionCanvas = Instantiate(_settings.gameInteractionCanvasPrefab);
        _gameInteractionCanvas.SetBlockDuration(_settings.animationDuration);
    }

    private void GenerateVisuals()
    {
        Vector3 startPosition = Vector3.right * _settings.size + Vector3.up * _settings.size;
        startPosition /= -2;
        startPosition.x += .5f;
        for (int i = 0; i < _settings.size ; i++)
        {
            for (int j = 0; j < _settings.size; j++)
            {
                SetupTileAtPosition(i, j, startPosition+ Vector3.right *j + Vector3.up* i);
            }

        }
    }

    private void SetupTileAtPosition(int i, int j, Vector3 pos)
    {
        _tileVisuals[i, j] = Instantiate(_settings.tilePrefab, pos, quaternion.identity);
        _tileVisuals[i, j].transform.SetParent(transform);
        _tileVisuals[i,j].SetSettings(_settings);
        _tileVisuals[i,j].SetColor(TileUtils.TileTypeToColor(_board.Tiles[i,j].Type));
        _tileVisuals[i,j].SetPos(new Vector2Int(i,j));
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
        Vector3 startPosition = Vector3.right * _settings.size + Vector3.up * _settings.size;
        startPosition /= -2;
        for (int i = 0; i < _settings.size ; i++)
        {
            for (int j = 0; j < _settings.size; j++)
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
    private void ShuffleUpdateBoard()
    {
        for (int i = 0; i < _settings.size; i++)
        {
            for (int j = 0; j < _settings.size; j++)
            {
                _tileVisuals[i,j].SetColor(TileUtils.TileTypeToColor(_board.Tiles[i,j].Type));
            }
        }
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
    private void OnEnable()
    {
        Match3Actions.onClickTile += ClickedTileAtPosition;
        Match3Actions.onTilesDestroyed += DestroyTiles;
        Match3Actions.onExistingTilesDropped += MoveExistingDroppedTiles;
        Match3Actions.onTilesFilledFromAbove += MoveNewDroppedTiles;
        Match3Actions.onBoardShuffle += ShuffleUpdateBoard;
    }



    private void OnDisable()
    {
        Match3Actions.onClickTile -= ClickedTileAtPosition;
        Match3Actions.onTilesDestroyed -= DestroyTiles;
        Match3Actions.onExistingTilesDropped -= MoveExistingDroppedTiles;
        Match3Actions.onTilesFilledFromAbove -= MoveNewDroppedTiles;
        Match3Actions.onBoardShuffle -= ShuffleUpdateBoard;
    }

}

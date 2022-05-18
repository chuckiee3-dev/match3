using System;
using System.Collections.Generic;
using UnityEngine;

public static class Match3Actions
{
   public static Action<List<Vector2Int>> onTilesDestroyed;
   public static Action<Vector2Int> onClickTile;
   public static Action onUsedMove;
   public static Action onExistingTilesDropped;
   public static Action<List<Vector2Int>> onTilesFilledFromAbove;
   public static Action onBoardShuffle;
   
   
   public static Action<Dictionary<TileType, int>> onTileTypeOfAmountDestroyed;

   
   public static void TilesDestroyed(List<Vector2Int> positions)
   {
      onTilesDestroyed?.Invoke(positions);
   }

   public static void ClickedTile(Vector2Int pos)
   {
      onClickTile.Invoke(pos);
   }

   public static void UsedMove()
   {
      onUsedMove?.Invoke();
   }
   public static void ExistingTilesDropped()
   {
      onExistingTilesDropped.Invoke();
   }

   public static void TilesFilledFromAbove(List<Vector2Int> filledTilePositions)
   {
      onTilesFilledFromAbove?.Invoke(filledTilePositions);
   }
   public static void BoardShuffle()
   {
      onBoardShuffle?.Invoke();
   }
   
   public static void TileTypeOfAmountDestroyed(Dictionary<TileType, int> destroyedDict)
   {
      onTileTypeOfAmountDestroyed?.Invoke(destroyedDict);
   }
}

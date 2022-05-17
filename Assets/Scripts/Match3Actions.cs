using System;
using System.Collections.Generic;
using UnityEngine;

public static class Match3Actions
{
   public static Action<List<Vector2Int>> onTilesDestroyed;
   public static Action<Vector2Int> onClickTile;
   public static Action onExistingTilesDropped;
   public static Action<List<Vector2Int>> onTilesFilledFromAbove;

   public static void TilesDestroyed(List<Vector2Int> positions)
   {
      onTilesDestroyed?.Invoke(positions);
   }

   public static void ClickedTile(Vector2Int pos)
   {
      onClickTile.Invoke(pos);
   }
   public static void ExistingTilesDropped()
   {
      onExistingTilesDropped.Invoke();
   }

   public static void TilesFilledFromAbove(List<Vector2Int> filledTilePositions)
   {
      onTilesFilledFromAbove?.Invoke(filledTilePositions);
   }
}

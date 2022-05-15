using System;
using System.Collections.Generic;
using UnityEngine;

public static class Match3Actions
{
   public static Action<List<Vector2Int>> OnTilesDestroyed;
   public static Action<Vector2Int> OnClickTile;
   public static Action OnExistingTilesDropped;

   public static void TilesDestroyed(List<Vector2Int> positions)
   {
      OnTilesDestroyed?.Invoke(positions);
   }

   public static void ClickedTile(Vector2Int pos)
   {
      OnClickTile.Invoke(pos);
   }
   public static void ExistingTilesDropped()
   {
      OnExistingTilesDropped.Invoke();
   }
}

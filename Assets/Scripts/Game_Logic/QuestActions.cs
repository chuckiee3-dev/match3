using System;
using System.Collections.Generic;

public static class QuestActions
{
    public static Action<Dictionary<TileType, int>> onQuestUpdated;
    public static Action<int> onMoveCountUpdated;
    public static Action onQuestCompleted;
    public static Action onOutOfMoves;

    public static void QuestUpdated(Dictionary<TileType, int> currentQuestStatus)
    {
        onQuestUpdated?.Invoke(currentQuestStatus);    
    }

    public static void OutOfMoves()
    {
        onOutOfMoves?.Invoke();
    }

    public static void QuestCompleted()
    {
        onQuestCompleted?.Invoke();
    }

    public static void MoveCountUpdated(int amountLeft)
    {
        onMoveCountUpdated?.Invoke(amountLeft);
    }
}

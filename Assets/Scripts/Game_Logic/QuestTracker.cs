using System.Collections.Generic;
using UnityEngine;

public class QuestTracker
{
    private int _movesRemaining;
    private Dictionary<TileType, int> _questStatus;
    
    public int MovesRemaining => _movesRemaining;
    
    public QuestTracker(Quest quest)
    {
        _movesRemaining = quest.totalMoves;
        
        _questStatus = new Dictionary<TileType, int>();

        foreach (var item in quest.questItems)
        {
            if(!_questStatus.ContainsKey(item.TypeToDestroy)){
                _questStatus.Add(item.TypeToDestroy, item.countToDestroy);
            }
        }
    }


    private void UpdateQuestStatus(Dictionary<TileType, int> destroyedTileDict)
    {
        bool updated = false;
        foreach (var key in destroyedTileDict.Keys)
        {
            if (_questStatus.ContainsKey(key) && _questStatus[key] != 0)
            {
                _questStatus[key] -= destroyedTileDict[key];
                if (_questStatus[key] <= 0)
                {
                    _questStatus[key] = 0;
                    _questStatus.Remove(key);
                }

                updated = true;
            }
        }
        
        if (updated)
        {
            if(_questStatus.Keys.Count != 0 && _movesRemaining <= 0){
                QuestActions.QuestUpdated(_questStatus);
                QuestActions.OutOfMoves();
            }else if (_questStatus.Keys.Count != 0 && _movesRemaining > 0)
            {
                QuestActions.QuestUpdated(_questStatus);
            }
            else
            {
                QuestActions.QuestCompleted();
            }
        }
    }
    
    private void ReduceMoves()
    {
        if(_movesRemaining == 0) return;
        _movesRemaining--;
        QuestActions.MoveCountUpdated(_movesRemaining);
    }
    public void StartTracking()
    {
        Match3Actions.onTileTypeOfAmountDestroyed += UpdateQuestStatus;
        Match3Actions.onUsedMove += ReduceMoves;
    }

    public void StopTracking()
    {
        Match3Actions.onTileTypeOfAmountDestroyed -= UpdateQuestStatus;
        Match3Actions.onUsedMove -= ReduceMoves;
    }


}

using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Transform itemUIParent;
    [SerializeField] private QuestItemUI itemUIPrefab;
    [SerializeField] private MovesLeftUI movesLeftUIPrefab;
    
    [SerializeField] private Canvas winUI;
    [SerializeField] private Canvas failUI;

    
    private MovesLeftUI _movesLeftUI;
    
    private Dictionary<TileType, QuestItemUI> _questItemUIs;
    public void Setup(Quest quest, SpritePalette palette)
    {
        
        _movesLeftUI = Instantiate(movesLeftUIPrefab, itemUIParent);
        _movesLeftUI.SetMovesLeft(quest.totalMoves);
        _questItemUIs = new Dictionary<TileType, QuestItemUI>();
        foreach (var questItem in quest.questItems)
        {
            if (!_questItemUIs.ContainsKey(questItem.TypeToDestroy))
            {
                var item = Instantiate(itemUIPrefab, itemUIParent);
                item.Setup(questItem, palette);
                _questItemUIs.Add(questItem.TypeToDestroy, item);
            }
        }

    }
    private void UpdateUI(Dictionary<TileType, int> remainingItems)
    {
        foreach (var key in _questItemUIs.Keys)
        {
            if (!remainingItems.ContainsKey(key))
            {
                _questItemUIs[key].Complete();
            }
            else
            {
                _questItemUIs[key].UpdateAmount(remainingItems[key]);
            }
        }
    }
    private void ShowFailUI()
    {
        failUI.enabled = true;
    }

    private void ShowCompleteUI()
    {
        foreach (var key in _questItemUIs.Keys)
        {
            _questItemUIs[key].Complete();
        }

        winUI.enabled = true;
    }

    
    private void OnEnable()
    {
        QuestActions.onQuestUpdated += UpdateUI;
        QuestActions.onOutOfMoves += ShowFailUI;
        QuestActions.onQuestCompleted += ShowCompleteUI;
    }
    
    private void OnDisable()
    {
        QuestActions.onOutOfMoves -= ShowFailUI;
        QuestActions.onQuestUpdated -= UpdateUI;
        QuestActions.onQuestCompleted -= ShowCompleteUI;
    }
}

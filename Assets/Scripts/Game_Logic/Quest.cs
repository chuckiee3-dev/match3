using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
   [SerializeField] private int _totalMoves = 20;
   [SerializeField] private List<QuestItem> _questItems;

   public List<QuestItem> questItems
   {
      get => _questItems;
      set => _questItems = value;
   }

   public int totalMoves => _totalMoves;

}

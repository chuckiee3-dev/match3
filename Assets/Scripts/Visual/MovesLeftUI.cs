using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesLeftUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesLeftTMP;

    public void SetMovesLeft(int amount)
    {
        movesLeftTMP.text = amount.ToString();
    }

    private void OnEnable()
    {
        QuestActions.onMoveCountUpdated += SetMovesLeft;
    }
    private void OnDisable()
    {
        QuestActions.onMoveCountUpdated -= SetMovesLeft;
    }
}

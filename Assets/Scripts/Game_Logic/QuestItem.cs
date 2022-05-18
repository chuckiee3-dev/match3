using System;
using UnityEngine;

[Serializable]
public class QuestItem
{
    [SerializeField] private int _countToDestroy;
    [SerializeField] private TileType typeToDestroy;

    public TileType TypeToDestroy
    {
        get => typeToDestroy;
        set => typeToDestroy = value;
    }

    public int countToDestroy => _countToDestroy;
}

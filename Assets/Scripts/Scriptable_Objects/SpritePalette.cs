using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Palette", menuName = "Match3D/Color Palette", order = 0)]
public class SpritePalette : ScriptableObject
{
    [SerializeField] private TileVisualData defaultData;
    [SerializeField] private List<TileType> allVisualTileTypes;
    [SerializeField] private List<TileVisualData> visualData;

    public TileVisualData GetDataForType(TileType type)
    {
        int index = allVisualTileTypes.IndexOf(type);
        if (index < 0 || index >= visualData.Count) return defaultData;
        return visualData[index];
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (allVisualTileTypes == null)
        {
            allVisualTileTypes = new List<TileType>();
        }

        allVisualTileTypes.Clear();
        
        foreach(TileType type in Enum.GetValues(typeof(TileType)))
        {
            if (type == TileType.Empty || type == TileType.None) continue;
            allVisualTileTypes.Add(type);
        }

        if (allVisualTileTypes.Count != visualData.Count)
        {
            Debug.LogWarning("Type and visual representation count are not equal size. " +
                             "Some tiles will use default sprite");
        }
    }
    #endif
}

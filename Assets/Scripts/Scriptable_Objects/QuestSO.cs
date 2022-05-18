using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Match3D/Quest", order = 0)]
public class QuestSO : ScriptableObject
{
    public Quest quest;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        var size = quest.questItems.Count;
        for (int i = 0; i < size; i++)
        {
            if (quest.questItems[i].TypeToDestroy == TileType.Empty ||
                quest.questItems[i].TypeToDestroy == TileType.None)
            {
                size--;
            }
        }
    }
#endif
}

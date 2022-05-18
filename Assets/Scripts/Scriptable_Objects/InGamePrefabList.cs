using UnityEngine;

[CreateAssetMenu(fileName = "New Prefab List", menuName = "Match3D/Prefab List", order= 1)]
public class InGamePrefabList : ScriptableObject
{
    [SerializeField] private TileVisual _tilePrefab;
    [SerializeField] private GameInteractionCanvas _gameInteractionCanvasPrefab;
    [SerializeField] private QuestUI _questUIPrefab;


    public TileVisual tilePrefab => _tilePrefab;

    public GameInteractionCanvas gameInteractionCanvasPrefab => _gameInteractionCanvasPrefab;

    public QuestUI questUIPrefab => _questUIPrefab;
}

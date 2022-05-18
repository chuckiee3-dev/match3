using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Match3D/Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
   [Header("Board")]
   [Range(5,9)]
   [SerializeField] private int boardSize = 5;
   [SerializeField] private TileType[] _tileTypes = { TileType.Red , TileType.Green, TileType.Blue, TileType.Yellow};
   [SerializeField] private InGamePrefabList prefabList;
   [SerializeField] private SpritePalette _palette;
   [Space(5)]
   [Header("Tile Animations")]
   [SerializeField] private float _animationDuration;
   [SerializeField] private Ease _ease;
   public int size => boardSize;
   
   public TileType[] tileTypes => _tileTypes;
   public TileVisual tilePrefab => prefabList.tilePrefab;
   public GameInteractionCanvas gameInteractionCanvasPrefab => prefabList.gameInteractionCanvasPrefab;
   public QuestUI questUIPrefab => prefabList.questUIPrefab;
   public SpritePalette palette => _palette;
   public float animationDuration => _animationDuration;
   public Ease ease=> _ease;

   public TileVisualData GetDataForType(TileType type)
   {
      return _palette.GetDataForType(type);
   }
}

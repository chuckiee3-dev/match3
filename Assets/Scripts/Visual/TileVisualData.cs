using UnityEngine;
[CreateAssetMenu(fileName = "New Tile Visual Data", menuName = "Match3D/Tile Visual Data", order = 0)]
public class TileVisualData: ScriptableObject
{
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Sprite _sprite = null;
    
    public Color color => _color;
    public Sprite sprite => _sprite;
}

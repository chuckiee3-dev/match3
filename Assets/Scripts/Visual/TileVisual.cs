using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    private GameSettings _settings;
    private SpriteRenderer _sr;
    private Vector2Int _pos;
    private Collider2D _collider;

    public void SetSettings(GameSettings settings)
    {
        _settings = settings;
    }
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void SetVisual(TileVisualData data)
    {
        _sr.color = data.color;
        _sr.sprite = data.sprite;
    }

    public void SetPos(Vector2Int pos)
    {
        _pos = pos;
    }
    public void GetDestroyed()
    {
        _sr.color = Color.clear;
        _collider.enabled = false;
    }
    public void DropBy(int tileDropAmount)
    {
        _pos.x -= tileDropAmount;
        AnimateDropBy(tileDropAmount);
    }
    public void AnimateDropBy(int tileDropAmount)
    {
        transform.DOMoveY(transform.position.y - tileDropAmount, _settings.animationDuration).SetEase(_settings.ease);
    }
    public void Clicked()
    {
        Match3Actions.ClickedTile(_pos);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.back, _pos.ToString());
    }
#endif
}

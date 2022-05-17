using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Vector2Int _pos;
    private Collider2D _collider;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void SetColor(Color color)
    {
        _sr.color = color;
    }

    public void SetPos(Vector2Int pos)
    {
        _pos = pos;
    }
    public void GetDestroyed()
    {
        SetColor(Color.clear);
        _collider.enabled = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on: "+ _pos.ToString());
        Match3Actions.ClickedTile(_pos);
    }
    public void DropBy(int tileDropAmount)
    {
        _pos.x -= tileDropAmount;
        AnimateDropBy(tileDropAmount);
    }
    public void AnimateDropBy(int tileDropAmount)
    {
        transform.DOMoveY(transform.position.y - tileDropAmount, .4f);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.back, _pos.ToString());
    }
#endif
}

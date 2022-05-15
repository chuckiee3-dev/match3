using System;
using UnityEditor;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Vector2Int _pos;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
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
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on: "+ _pos.ToString());
        Match3Actions.ClickedTile(_pos);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.back, _pos.ToString());
    }
#endif
}

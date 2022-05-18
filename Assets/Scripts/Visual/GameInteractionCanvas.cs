using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInteractionCanvas : MonoBehaviour
{
    private Image _img;
    private WaitForSeconds _blockWait;
    private Camera _camera;
    private bool _outOfMoves;
    private void Awake()
    {
        _img = GetComponentInChildren<Image>();
        _img.raycastTarget = true;
        _camera = Camera.main;
        _outOfMoves = false;
    }

    public void UserClickReceived()
    {
        if(_outOfMoves) return;
        Vector3 pos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(pos);
        var hit = Physics2D.GetRayIntersection(ray, 10);
        if (!hit) return;
        if (hit.transform.TryGetComponent(out TileVisual visual))
        {
            visual.Clicked();
        }
    }
    
    private void OnEnable()
    {
        Match3Actions.onTilesDestroyed += DisableClicks;
        QuestActions.onOutOfMoves += SetOutOfMoves;
    }

    private void OnDisable()
    {
        Match3Actions.onTilesDestroyed -= DisableClicks;
        QuestActions.onOutOfMoves -= SetOutOfMoves;
    }

    private void SetOutOfMoves()
    {
        _outOfMoves = true;
    }

    private void DisableClicks(List<Vector2Int> obj)
    {
        _img.raycastTarget = false;
        StartCoroutine(EnableClickAfterDelay());
    }

    private IEnumerator EnableClickAfterDelay()
    {
        yield return _blockWait;
        _img.raycastTarget = true;
    }
    
    public void SetBlockDuration(float duration)
    {
        _blockWait = new WaitForSeconds(duration);
    }
}

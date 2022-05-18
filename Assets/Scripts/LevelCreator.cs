using System;
using JetBrains.Annotations;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private BoardBehaviour boardPrefab;
    [SerializeField] private QuestSO quest;

    private BoardBehaviour _board;
    private QuestTracker _tracker;
    private QuestUI _questUI;
    [CanBeNull] private Camera _camera;
    private void Awake()
    {
        _board = Instantiate(boardPrefab);
        _board.Setup(settings);
        _tracker = new QuestTracker(quest.quest);
        _questUI = Instantiate(settings.questUIPrefab);
        _questUI.Setup(quest.quest);
        _camera = Camera.main;
        _camera.orthographicSize = settings.size;
    }

    private void OnEnable()
    {
        _tracker.StartTracking();
    }

    private void OnDisable()
    {
        _tracker.StopTracking();
    }
}

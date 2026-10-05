using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    private Transform _player;
    private float _roadSpeed;
    private int _basePoolSizeRoad;
    private GameUI _gameUI;
    private GameStateMachine _gameStateMachine;
    private RoadSpawner _roadSpawner;
    private LaneSelector _laneSelector;
    private RoadMover _roadMover;
    private ScoreService _scoreService;
    public void Initialize(Transform player, float roadSpeed, int basePoolSizeRoad, GameUI gameUI, GameStateMachine gameStateMachine,
        RoadSpawner roadSpawner, LaneSelector laneSelector, RoadMover roadMover, ScoreService scoreService)
    {
        _player = player;
        _roadSpeed = roadSpeed;
        _basePoolSizeRoad = basePoolSizeRoad;
        _gameUI = gameUI;
        _gameStateMachine = gameStateMachine;
        _laneSelector = laneSelector;
        _roadMover = roadMover;
        _scoreService = scoreService;
        _roadSpawner = roadSpawner;
    }    
    void Start()
    {
        for (int i = 0; i < _basePoolSizeRoad; i++)
        {
            _roadSpawner.SpawnNextSegment();
        }
        _gameUI.OnStateChanged(_gameStateMachine.CurrentState);
        _gameUI.UpdateScore(0);
    }
    public void OnLeftPressed()
    {
        if (_gameStateMachine.CurrentState != GameState.Running)
        {
            return;
        }
        bool moved = _laneSelector.TryMove(-1);
        if (moved)
        {
            _player.position = _laneSelector.CurrentPosition;
        }
    }
    public void OnRightPressed()
    {
        if (_gameStateMachine.CurrentState != GameState.Running)
        {
            return;
        }
        bool moved = _laneSelector.TryMove(1);
        if (moved)
        {
            _player.position = _laneSelector.CurrentPosition;
        }
    }
    private void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.Running)
        {
            _roadMover.Tick(Time.deltaTime);
            _scoreService.AddDistance(_roadSpeed * Time.deltaTime);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

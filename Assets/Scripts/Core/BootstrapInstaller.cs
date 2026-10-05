using UnityEngine;

public class BootstrapInstaller : MonoBehaviour
{
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private int _segmentsBetweenBonuses = 2;
    [SerializeField] private float _roadSpeed = 10f;
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private int _basePoolSizeObstacle = 40;
    [SerializeField] private int _basePoolSizeRoad = 7;
    [SerializeField] private int _basePoolSizeBonus = 10;
    [SerializeField] private float _roadLinesOffset;
    [SerializeField] private float _minObstacleGap;
    [SerializeField] private float _maxObstacleGap;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _obstacleParent;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _roadParent;
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private GameObject _bonusPrefab;
    [SerializeField] private GameObject _bonusParent;
    [SerializeField] private GameUI _gameUI;
    private GameStateMachine _gameStateMachine;
    private RoadLines _roadLines;
    private InputService _inputService;
    private ScoreService _scoreService;
    private LaneSelector _laneSelector;
    private RoadMover _roadMover;
    private PrefabPool _obstaclePrefabPool;
    private PrefabPool _roadPrefabPool;
    private PrefabPool _bonusPrefabPool;
    private RoadSpawner _roadSpawner;
    private BonusSpawner _bonusSpawner;
    private ObstacleLayoutGenerator _obstacleLayoutGenerator;
    private ObstacleSpawner _obstacleSpawner;
    private void Awake()
    {
        _gameStateMachine = new GameStateMachine();
        _roadLines = new RoadLines(_roadLinesOffset);
        _laneSelector = new LaneSelector(_roadLines);
        _inputService = new InputService();
        _scoreService = new ScoreService();
        _player.position = _laneSelector.CurrentPosition;
        _obstaclePrefabPool = new PrefabPool(_obstaclePrefab, _basePoolSizeObstacle, _obstacleParent);
        _roadPrefabPool = new PrefabPool(_roadPrefab, _basePoolSizeRoad, _roadParent.transform);
        _bonusPrefabPool = new PrefabPool(_bonusPrefab, _basePoolSizeBonus, _bonusParent.transform);
        _roadMover = new RoadMover(_roadSpeed, 0f);
        _obstacleLayoutGenerator = new ObstacleLayoutGenerator(_minObstacleGap, _maxObstacleGap, _roadLines.Count);
        _obstacleSpawner = new ObstacleSpawner(_obstaclePrefabPool, _roadLines, _obstacleLayoutGenerator);
        _bonusSpawner = new BonusSpawner(_bonusPrefabPool, _roadLines, _segmentsBetweenBonuses);
        _roadSpawner = new RoadSpawner(_roadPrefabPool, _roadMover, _obstacleSpawner, _bonusSpawner);
        _roadMover.SegmentPassed += _roadSpawner.OnSegmentPassed;
        _playerCollision.Died += _gameStateMachine.OnPlayerDied;
        _gameStateMachine.RestartRequested += _gameLoop.Restart;
        _inputService.Enable();
        _inputService.LeftPressed += _gameLoop.OnLeftPressed;
        _inputService.RightPressed += _gameLoop.OnRightPressed;
        _inputService.Tapped += _gameStateMachine.OnTap;
        _gameStateMachine.StateChanged += _gameUI.OnStateChanged;
        _scoreService.ScoreChanged += _gameUI.UpdateScore;
        _playerCollision.CoinCollected += _scoreService.AddBonus;
        _gameLoop.Initialize(_player, _roadSpeed, _basePoolSizeRoad, _gameUI,
            _gameStateMachine, _roadSpawner, _laneSelector, _roadMover, _scoreService);
    }

    
    private void OnDestroy()
    {
        _inputService.LeftPressed -= _gameLoop.OnLeftPressed;
        _inputService.RightPressed -= _gameLoop.OnRightPressed;
        _inputService.Tapped -= _gameStateMachine.OnTap;
        _playerCollision.Died -= _gameStateMachine.OnPlayerDied;
        _scoreService.ScoreChanged -= _gameUI.UpdateScore;
        _gameStateMachine.StateChanged -= _gameUI.OnStateChanged;
        _roadMover.SegmentPassed -= _roadSpawner.OnSegmentPassed;
        _playerCollision.CoinCollected -= _scoreService.AddBonus;
        _gameStateMachine.RestartRequested -= _gameLoop.Restart;
        _inputService.Dispose();
    }
}

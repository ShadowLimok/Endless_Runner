using UnityEngine;

public class RoadSpawner
{
    private PrefabPool _roadPrefabPool;
    private RoadMover _roadMover;
    private RoadSegment _lastSegment;
    private ObstacleSpawner _obstacleSpawner;
    private BonusSpawner _bonusSpawner;

    public RoadSpawner(PrefabPool roadPrefabPool, RoadMover roadMover, ObstacleSpawner obstacleSpawner, BonusSpawner bonusSpawner)
    {
        _roadPrefabPool = roadPrefabPool;
        _roadMover = roadMover;
        _obstacleSpawner = obstacleSpawner;
        _bonusSpawner = bonusSpawner;
    }
    public void SpawnNextSegment()
    {
        GameObject roadSegmentObj = _roadPrefabPool.Get();
        RoadSegment roadSegment = roadSegmentObj.GetComponent<RoadSegment>();
        if (_lastSegment != null)
        {
            Vector3 newPosition = _lastSegment.transform.position + Vector3.forward * _lastSegment.Length;
            roadSegment.transform.position = newPosition;
            var placements = _obstacleSpawner.Populate(roadSegment);
            _bonusSpawner.Populate(roadSegment, placements);
        }
        else
        {
            roadSegment.transform.position = Vector3.zero;
        }
        _roadMover.AddSegment(roadSegment);
        _lastSegment = roadSegment;
    }
    public void OnSegmentPassed(RoadSegment segment)
    {
        _bonusSpawner.Clear(segment);
        _obstacleSpawner.Clear(segment);
        _roadPrefabPool.Release(segment.gameObject);
        SpawnNextSegment();
    }
}

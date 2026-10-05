using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner
{
    private PrefabPool _obstaclePrefabPool;
    private RoadLines _roadLines;
    private ObstacleLayoutGenerator _obstacleLayoutGenerator;

    public ObstacleSpawner(PrefabPool obstaclePrefabPool, RoadLines roadLines, 
        ObstacleLayoutGenerator obstacleLayoutGenerator)
    {
        _obstaclePrefabPool = obstaclePrefabPool;
        _roadLines = roadLines;
        _obstacleLayoutGenerator = obstacleLayoutGenerator;
    }
    public List<ObstaclePlacement> Populate(RoadSegment segment)
    {
        List<ObstaclePlacement> placements = _obstacleLayoutGenerator.GenerateObstaclePositions(segment.Length);
        foreach (ObstaclePlacement placement in placements)
        {
            GameObject obstacle = _obstaclePrefabPool.Get();
            obstacle.transform.SetParent(segment.transform);
            Vector3 position = new Vector3(_roadLines.GetPosition(placement.Lane).x, 0, placement.Z - segment.Length/2);
            obstacle.transform.localPosition = position;
            segment.Add(obstacle);
        }
        return placements;
    }
    public void Clear(RoadSegment segment)
    {
        foreach (GameObject obstacle in segment.Obstacles)
        {
            _obstaclePrefabPool.Release(obstacle);
        }
        segment.Clear();
    }
}

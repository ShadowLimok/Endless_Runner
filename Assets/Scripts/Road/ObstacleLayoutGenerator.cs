using System.Collections.Generic;
using UnityEngine;

public class ObstacleLayoutGenerator
{
    private float _minGap;
    private float _maxGap;
    private int _laneCount;

    public ObstacleLayoutGenerator(float minGap, float maxGap, int laneCount)
    {
        if (minGap <= 0f)
            throw new System.ArgumentException("minGap = 0!");
        if (maxGap < minGap)
            throw new System.ArgumentException("maxGap < minGap!");
        if (laneCount <= 0)
            throw new System.ArgumentException("laneCount = 0!");

        _minGap = minGap;
        _maxGap = maxGap;
        _laneCount = laneCount;
    }
    public List<ObstaclePlacement> GenerateObstaclePositions(float segmentLength)
    {
        List<ObstaclePlacement> obstaclePositions = new List<ObstaclePlacement>();
        float currentPosition = 0f;
        while (true)
        {
            float step = Random.Range(_minGap, _maxGap);
            currentPosition += step;

            if (currentPosition >= segmentLength)
            {
                break;
            }
            int lane = Random.Range(0, _laneCount);
            obstaclePositions.Add(new ObstaclePlacement(lane, currentPosition));
        }
        return obstaclePositions;
    }
}

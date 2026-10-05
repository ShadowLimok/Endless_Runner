using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadMover
{
    private List<RoadSegment> _roadSegments = new List<RoadSegment>();
    private float _speed;
    private float _despawnZ;
    public event Action<RoadSegment> SegmentPassed;

    public RoadMover(float speed, float despawnZ)
    {
        _speed = speed;
        _despawnZ = despawnZ;
    }
    public void AddSegment(RoadSegment segment)
    {
        _roadSegments.Add(segment);
    }
    public void Tick(float deltaTime)
    {
        for (int i = _roadSegments.Count - 1; i >= 0; i--)
        {
            RoadSegment segment = _roadSegments[i];
            segment.transform.position += Vector3.back * _speed * deltaTime;
            if(segment.transform.position.z + segment.Length < _despawnZ)
            {
                _roadSegments.RemoveAt(i);
                SegmentPassed?.Invoke(segment);
            }
        }
    }
}

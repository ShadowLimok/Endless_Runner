using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner
{
    private PrefabPool _bonusPrefabPool;
    private RoadLines _roadLines;
    private int _segmentsBetweenBonuses;
    private int _segmentCount;

    public BonusSpawner(PrefabPool bonusPrefabPool, RoadLines roadLines, int segmentsBetweenBonuses)
    {
        _bonusPrefabPool = bonusPrefabPool;
        _roadLines = roadLines;
        _segmentsBetweenBonuses = segmentsBetweenBonuses;
    }
    public void Populate(RoadSegment segment, List<ObstaclePlacement> placements)
    {
        _segmentCount++;
        if (_segmentCount < _segmentsBetweenBonuses) return;
        if (placements.Count < 2) return;
        int i = Random.Range(0, placements.Count - 1);
        float z = (placements[i].Z + placements[i + 1].Z) / 2f;
        int lane = Random.Range(0, _roadLines.Count);
        GameObject bonus = _bonusPrefabPool.Get();
        bonus.transform.SetParent(segment.transform);
        bonus.transform.localPosition = new Vector3(_roadLines.GetPosition(lane).x, 0.5f, z - segment.Length/2);
        segment.AddBonus(bonus);
        _segmentCount = 0;
    }
    public void Clear(RoadSegment segment)
    {
        foreach (var bonus in segment.Bonuses)
        {
            _bonusPrefabPool.Release(bonus);
        }
    }
}

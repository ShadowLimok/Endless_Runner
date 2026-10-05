using System.Collections.Generic;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    public IReadOnlyList<GameObject> Obstacles => _obstacles;
    public IReadOnlyList<GameObject> Bonuses => _bonuses;
    public float Length => _length;
    [SerializeField] private float _length;
    private List<GameObject> _bonuses = new List<GameObject>();
    private List<GameObject> _obstacles = new List<GameObject>();
    public void Add(GameObject obstacle)
    {
        _obstacles.Add(obstacle);
    }
    public void AddBonus(GameObject bonus)
    {
        _bonuses.Add(bonus);
    }
    public void Clear()
    {
        _obstacles.Clear();
        _bonuses.Clear();
    }
}

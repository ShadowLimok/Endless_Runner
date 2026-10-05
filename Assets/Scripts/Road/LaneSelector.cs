using UnityEngine;

public class LaneSelector
{
    public int CurrentIndex => _current;
    public Vector3 CurrentPosition => _roadLines.GetPosition(_current);
    private RoadLines _roadLines;
    private int _current;

    public LaneSelector(RoadLines roadLines)
    {
        _roadLines = roadLines;
        _current = _roadLines.Count / 2;
    }
    public bool TryMove(int direction)
    {
        int newIndex = _current + direction;
        if (newIndex >= 0 && newIndex < _roadLines.Count)
        {
            _current = newIndex;
            return true;
        }
        return false;
    }
}

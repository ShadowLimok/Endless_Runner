using System.Collections.Generic;
using UnityEngine;

public class RoadLines
{
    public int Count => _roadLines.Count;
    public Vector3 GetPosition(int index) => _roadLines[index];
    private List<Vector3> _roadLines;
    private float _offset;

    public RoadLines(float offset)
    {
        _offset = offset;
        _roadLines = new List<Vector3>() { new Vector3(-_offset, 0, 0),
            new Vector3(0,0,0), new Vector3(_offset, 0,0)};
    } 
}

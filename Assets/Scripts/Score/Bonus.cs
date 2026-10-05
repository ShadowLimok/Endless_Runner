using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int Points => _points;
    [SerializeField] private int _points;
}

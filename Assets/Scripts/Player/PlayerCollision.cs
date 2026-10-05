using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action Died;
    public event Action<int> CoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Obstacle>(out _))
        {
            Died?.Invoke();
        }
        else if(other.TryGetComponent<Bonus>(out var bonus))
        {
            other.gameObject.SetActive(false);
            CoinCollected?.Invoke(bonus.Points);
        }
    }
}

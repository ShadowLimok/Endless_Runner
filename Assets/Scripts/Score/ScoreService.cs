using System;

public class ScoreService
{
    public event Action<int> ScoreChanged;
    public int Score => (int)_distance + _bonusScore;
    private int _bonusScore;
    private float _distance;
    private int _lastShownScore;
    public void AddDistance(float amount)
    {
        _distance += amount;
        NotifyIfChanged();
    }
    public void AddBonus(int points)
    {
        _bonusScore += points;
        NotifyIfChanged();
    }
    private void NotifyIfChanged()
    {
        int currentScore = Score;
        if (currentScore != _lastShownScore)
        {
            _lastShownScore = currentScore;
            ScoreChanged?.Invoke(currentScore);
        }
    }
}

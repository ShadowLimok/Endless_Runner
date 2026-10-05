using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _readyToStartUI;
    [SerializeField] private GameObject _runningUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
        _finalScoreText.text = score.ToString();
    }
    public void OnStateChanged(GameState state)
    {
        _readyToStartUI.SetActive(state == GameState.ReadyToRun);
        _runningUI.SetActive(state == GameState.Running);
        _gameOverUI.SetActive(state == GameState.Dead);
    }
}

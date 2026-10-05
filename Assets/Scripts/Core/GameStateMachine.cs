using System;

public enum GameState
{
    ReadyToRun,
    Running,
    Dead
}
public class GameStateMachine
{
    public event Action RestartRequested;
    public event Action <GameState> StateChanged;
    public GameState CurrentState => _currentState;
    private GameState _currentState;
    public void ChangeState(GameState newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
        StateChanged?.Invoke(newState);
    }
    public void OnPlayerDied()
    {
        if(CurrentState == GameState.Running)
        {
            ChangeState(GameState.Dead);
        }
    }
    public void OnTap()
    {
        if(CurrentState == GameState.ReadyToRun)
        {
            ChangeState(GameState.Running);
        }
        else if(CurrentState == GameState.Dead)
        {
            RestartRequested?.Invoke();
        }
    }
}

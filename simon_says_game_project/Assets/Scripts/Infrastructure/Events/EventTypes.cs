namespace Infrastructure.Events
{
    public enum EventTypes
    {
        OnRivalTakeDamage,
        OnPlayerTakeDamage,
        OnPlayerAddHealth,
        OnRivalAddHealth,
        OnRivalTurn,
        OnPlayerTurn,
        OnRuneSelection,
        OnRuneSelectionEnd,
        OnPlayerSequenceSuccess,
        OnPlayerSequenceFailure,
        OnRivalDefeat,
        OnPlayerDeath,
        OnPlayerScoreChange,
        OnGameOverWin,
        OnGameTimerValueChange
    }
}
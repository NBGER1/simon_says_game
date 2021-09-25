namespace Infrastructure.Events
{
    public enum EventTypes
    {
        //# Gameplay
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
        OnGameTimerValueChange,
        OnPlayerZeroHealth,
        OnPlayerReady,
        OnRivalReady,
        OnPlayerNewLife,
        
        //# Infrastructure
        OnDatabaseLoad,
        
        //# UI
        OnUIButtonClick

    }
}
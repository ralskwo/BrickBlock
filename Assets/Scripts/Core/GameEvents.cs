using System;

public static class GameEvents
{
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnClickRemainingChanged;
    public static event Action<bool> OnGameEnd;

    public static void RaiseScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void RaiseClickRemainingChanged(int remain)
    {
        OnClickRemainingChanged?.Invoke(remain);
    }

    public static void RaiseGameEnd(bool isSuccess)
    {
        OnGameEnd?.Invoke(isSuccess);
    }
}

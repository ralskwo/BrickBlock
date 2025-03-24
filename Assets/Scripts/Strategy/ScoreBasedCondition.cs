public class ScoreBasedCondition : ISuccessCondition
{
    private int targetScore;

    public ScoreBasedCondition(int targetScore)
    {
        this.targetScore = targetScore;
    }

    public bool IsSuccess(int currentScore, int remainingClicks)
    {
        return currentScore >= targetScore;
    }
}

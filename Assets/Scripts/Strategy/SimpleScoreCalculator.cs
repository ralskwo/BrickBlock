public class SimpleScoreCalculator : IScoreCalculator
{
    public int CalculateScore(int removedBlockCount)
    {
        return removedBlockCount * 100;
    }
}

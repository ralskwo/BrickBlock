using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GridManager GridManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public ClickHandler ClickHandler { get; private set; }

    [SerializeField] private int targetScore = 1000;

    private int currentScore = 0;

    private IScoreCalculator scoreCalculator;
    private ISuccessCondition successCondition;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GridManager = GetComponent<GridManager>();
        UIManager = GetComponent<UIManager>();
        ClickHandler = GetComponent<ClickHandler>();

        UIManager.SetTargetScore(targetScore);

        // 전략 객체 생성
        scoreCalculator = new SimpleScoreCalculator();
        successCondition = new ScoreBasedCondition(targetScore);
    }

    public void AddRemovedBlocks(int count)
    {
        int gainedScore = scoreCalculator.CalculateScore(count);
        currentScore += gainedScore;

        GameEvents.RaiseScoreChanged(currentScore);

        int remainingClicks = ClickHandler.RemainingClicks;
        if (remainingClicks <= 0)
        {
            bool isSuccess = successCondition.IsSuccess(currentScore, remainingClicks);
            GameEvents.RaiseGameEnd(isSuccess);
        }
    }


    public IEnumerator PlayClickEffectsAndDestroy(List<Block> matchedBlocks)
    {
        List<Coroutine> coroutines = new();
        foreach (var block in matchedBlocks)
            coroutines.Add(StartCoroutine(block.PlayClickEffect()));

        foreach (var coroutine in coroutines)
            yield return coroutine;

        foreach (var block in matchedBlocks)
            GridManager.ClearBlock(block.GridPosition);

        GetComponent<BlockFallHandler>().HandleFall();

        yield return new WaitForSeconds(0.05f);

        foreach (var block in matchedBlocks)
            Destroy(block.gameObject);

        AddRemovedBlocks(matchedBlocks.Count);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

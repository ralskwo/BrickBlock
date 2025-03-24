// UIManager.cs
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clickText;
    [SerializeField] private TextMeshProUGUI targetScoreText;

    private ClickHandler clickHandler;
    private int currentScore = 0;
    private int currentClicks = 0;
    private int targetScore = 0;

    public void InjectClickHandler(ClickHandler handler)
    {
        clickHandler = handler;
    }

    public void SetTargetScore(int target)
    {
        targetScore = target;
        if (targetScoreText != null)
            targetScoreText.text = $"Target Score: {target}";
    }

    private void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScore;
        GameEvents.OnClickRemainingChanged += UpdateClickCount;
        GameEvents.OnGameEnd += ShowResult;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
        GameEvents.OnClickRemainingChanged -= UpdateClickCount;
        GameEvents.OnGameEnd -= ShowResult;
    }

    private void Start()
    {
        if (clickHandler != null)
        {
            currentScore = 0;
            currentClicks = clickHandler.RemainingClicks;
            UpdateScore(currentScore);
            UpdateClickCount(currentClicks);
        }
    }

    private void UpdateScore(int score)
    {
        currentScore = score;
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void UpdateClickCount(int remaining)
    {
        currentClicks = remaining;
        if (clickText != null)
            clickText.text = $"Remain: {remaining}";
    }

    private void ShowResult(bool success)
    {
        if (success)
            successPanel.SetActive(true);
        else
            failPanel.SetActive(true);
    }
}
// ClickHandler.cs
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private int maxClicks = 5;

    public int ClickCount { get; private set; } = 0;
    public int RemainingClicks => maxClicks - ClickCount;

    void Start()
    {
        GameEvents.RaiseClickRemainingChanged(RemainingClicks);
    }

    public bool CanClick()
    {
        return ClickCount < maxClicks;
    }

    public void RegisterClick()
    {
        ClickCount++;
        GameEvents.RaiseClickRemainingChanged(RemainingClicks);
    }
}

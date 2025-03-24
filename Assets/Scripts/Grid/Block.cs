using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class Block : MonoBehaviour, IPointerClickHandler
{
    public int Value { get; private set; }
    public Vector2Int GridPosition { get; private set; }

    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Image backgroundImage;

    public void Setup(int value, Vector2Int pos)
    {
        Value = value;
        GridPosition = pos;
        UpdateVisual();
    }

    public void SetGridPosition(Vector2Int pos)
    {
        GridPosition = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.ClickHandler.CanClick())
        {
            GameManager.Instance.ClickHandler.RegisterClick();
            var matched = GameManager.Instance.GetComponent<BlockMatcher>().Match(this);
            StartCoroutine(GameManager.Instance.PlayClickEffectsAndDestroy(matched));
        }
    }

    public IEnumerator PlayClickEffect()
    {
        Transform target = transform;
        Vector3 originalScale = target.localScale;
        Vector3 punchScale = originalScale * 1.2f;

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            target.localScale = Vector3.Lerp(originalScale, punchScale, t);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            target.localScale = Vector3.Lerp(punchScale, originalScale, t);
            yield return null;
        }

        target.localScale = originalScale;
    }

    public IEnumerator AnimateMoveAtSpeed(Vector2 targetAnchoredPos, float speed = 800f)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startPos = rectTransform.anchoredPosition;
        float distance = Vector2.Distance(startPos, targetAnchoredPos);
        float duration = distance / speed;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetAnchoredPos, t);
            yield return null;
        }

        rectTransform.anchoredPosition = targetAnchoredPos;
    }

    private void UpdateVisual()
    {
        if (numberText != null)
            numberText.text = Value.ToString();

        if (backgroundImage != null)
            backgroundImage.color = GetColorByValue(Value);
    }

    private Color GetColorByValue(int value)
    {
        switch (value)
        {
            case 1: return new Color32(0, 255, 255, 255);
            case 2: return new Color32(0, 200, 0, 255);
            case 3: return new Color32(255, 100, 100, 255);
            case 4: return new Color32(255, 255, 100, 255);
            case 5: return new Color32(255, 150, 0, 255);
            case 6: return new Color32(150, 100, 255, 255);
            case 7: return new Color32(0, 150, 255, 255);
            case 8: return new Color32(255, 0, 150, 255);
            case 9: return new Color32(100, 255, 200, 255);
            case 10: return new Color32(180, 180, 180, 255);
            default: return Color.gray;
        }
    }
}

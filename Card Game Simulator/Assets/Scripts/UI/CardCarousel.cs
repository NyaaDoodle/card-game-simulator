using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardCarousel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Carousel Settings")]
    [SerializeField] private float snapSpeed = 5f;
    [SerializeField] private float snapThreshold = 50f;
    [SerializeField] private float cardSpacing = 200f;
    [SerializeField] private bool useSmoothing = true;

    [Header("Visual Effects")]
    [SerializeField] private float selectedScale = 1.2f;
    [SerializeField] private float unselectedScale = 0.8f;
    [SerializeField] private float scaleTransitionSpeed = 3f;
    [SerializeField] private AnimationCurve snapCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("References")]
    [SerializeField] private RectTransform contentTransform;
    [SerializeField] private RectTransform viewportTransform;

    private List<RectTransform> cardTransforms = new List<RectTransform>();
    private int currentSelectedIndex = 0;
    private bool isDragging = false;
    private bool isSnapping = false;
    private Vector2 lastDragPosition;
    private float dragVelocity = 0f;
    private Coroutine snapCoroutine;

    public System.Action<int> OnCardSelected;

    void Start()
    {
        initializeCarousel();
    }

    void Update()
    {
        if (!isDragging && !isSnapping)
        {
            UpdateCardVisuals();
        }
    }

    private void initializeCarousel()
    {
        // Get all card transforms
        cardTransforms.Clear();
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            cardTransforms.Add(contentTransform.GetChild(i).GetComponent<RectTransform>());
        }

        // Position cards initially
        positionCards();

        // Select the middle card initially
        if (cardTransforms.Count > 0)
        {
            currentSelectedIndex = Mathf.Clamp(cardTransforms.Count / 2, 0, cardTransforms.Count - 1);
            SnapToCard(currentSelectedIndex, false);
        }
    }

    private void positionCards()
    {
        for (int i = 0; i < cardTransforms.Count; i++)
        {
            float xPosition = i * cardSpacing;
            cardTransforms[i].anchoredPosition = new Vector2(xPosition, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        lastDragPosition = eventData.position;
        dragVelocity = 0f;

        // Stop any ongoing snapping
        if (snapCoroutine != null)
        {
            StopCoroutine(snapCoroutine);
            isSnapping = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 currentDragPosition = eventData.position;
        Vector2 deltaPosition = currentDragPosition - lastDragPosition;

        // Calculate velocity for momentum-based snapping
        dragVelocity = deltaPosition.x;

        // Move content
        Vector2 currentPos = contentTransform.anchoredPosition;
        contentTransform.anchoredPosition = new Vector2(
            currentPos.x + deltaPosition.x,
            currentPos.y
        );

        lastDragPosition = currentDragPosition;

        // Update selection during drag
        updateSelectionDuringDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Determine snap target based on velocity and position
        int targetIndex = calculateSnapTarget();
        SnapToCard(targetIndex, true);
    }

    private void updateSelectionDuringDrag()
    {
        int newSelectedIndex = getNearestCardIndex();
        if (newSelectedIndex != currentSelectedIndex)
        {
            currentSelectedIndex = newSelectedIndex;
            OnCardSelected?.Invoke(currentSelectedIndex);
        }
    }

    private int getNearestCardIndex()
    {
        float viewportCenter = viewportTransform.rect.width / 2f;
        float minDistance = float.MaxValue;
        int nearestIndex = 0;

        for (int i = 0; i < cardTransforms.Count; i++)
        {
            Vector3 cardWorldPos = cardTransforms[i].TransformPoint(Vector3.zero);
            Vector3 cardLocalPos = viewportTransform.InverseTransformPoint(cardWorldPos);

            float distance = Mathf.Abs(cardLocalPos.x - viewportCenter);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }

    private int calculateSnapTarget()
    {
        int nearestIndex = getNearestCardIndex();

        // Consider velocity for momentum-based snapping
        if (Mathf.Abs(dragVelocity) > snapThreshold)
        {
            if (dragVelocity > 0 && nearestIndex > 0)
            {
                nearestIndex = Mathf.Max(0, nearestIndex - 1);
            }
            else if (dragVelocity < 0 && nearestIndex < cardTransforms.Count - 1)
            {
                nearestIndex = Mathf.Min(cardTransforms.Count - 1, nearestIndex + 1);
            }
        }

        return nearestIndex;
    }

    private void SnapToCard(int cardIndex, bool animate = true)
    {
        if (cardIndex < 0 || cardIndex >= cardTransforms.Count) return;

        currentSelectedIndex = cardIndex;
        OnCardSelected?.Invoke(currentSelectedIndex);

        if (animate && useSmoothing)
        {
            snapCoroutine = StartCoroutine(AnimateSnapToCard(cardIndex));
        }
        else
        {
            SnapToCardImmediate(cardIndex);
        }
    }

    private void SnapToCardImmediate(int cardIndex)
    {
        float targetX = CalculateTargetPosition(cardIndex);
        contentTransform.anchoredPosition = new Vector2(targetX, contentTransform.anchoredPosition.y);
        UpdateCardVisuals();
    }

    private float CalculateTargetPosition(int cardIndex)
    {
        float viewportCenter = viewportTransform.rect.width / 2f;
        float cardPosition = cardIndex * cardSpacing;
        return viewportCenter - cardPosition;
    }

    private IEnumerator AnimateSnapToCard(int cardIndex)
    {
        isSnapping = true;

        float targetX = CalculateTargetPosition(cardIndex);
        float startX = contentTransform.anchoredPosition.x;
        float journey = 0f;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * snapSpeed;
            float easedProgress = snapCurve.Evaluate(journey);

            float currentX = Mathf.Lerp(startX, targetX, easedProgress);
            contentTransform.anchoredPosition = new Vector2(currentX, contentTransform.anchoredPosition.y);

            UpdateCardVisuals();
            yield return null;
        }

        // Ensure final position is exact
        contentTransform.anchoredPosition = new Vector2(targetX, contentTransform.anchoredPosition.y);
        UpdateCardVisuals();

        isSnapping = false;
        snapCoroutine = null;
    }

    private void UpdateCardVisuals()
    {
        float viewportCenter = viewportTransform.rect.width / 2f;

        for (int i = 0; i < cardTransforms.Count; i++)
        {
            RectTransform card = cardTransforms[i];

            // Calculate distance from center
            Vector3 cardWorldPos = card.TransformPoint(Vector3.zero);
            Vector3 cardLocalPos = viewportTransform.InverseTransformPoint(cardWorldPos);
            float distanceFromCenter = Mathf.Abs(cardLocalPos.x - viewportCenter);

            // Calculate scale based on distance
            float normalizedDistance = Mathf.Clamp01(distanceFromCenter / (cardSpacing / 2f));
            float targetScale = Mathf.Lerp(selectedScale, unselectedScale, normalizedDistance);

            // Smooth scale transition
            float currentScale = card.localScale.x;
            float newScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * scaleTransitionSpeed);
            card.localScale = Vector3.one * newScale;

            // Optional: Add alpha effect
            CanvasGroup canvasGroup = card.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                float targetAlpha = Mathf.Lerp(1f, 0.6f, normalizedDistance);
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * scaleTransitionSpeed);
            }
        }
    }
}

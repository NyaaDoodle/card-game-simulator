using UnityEngine;

public class PlayerHandDisplay : CardCollectionDisplay
{
    [Header("Cards in Hand Display Settings")]
    [SerializeField] private float fanSpreadFactor = -10f;
    [SerializeField] private float cardHorizontalSpacing = 150;
    [SerializeField] private float cardVerticalSpacing = 100;

    public Player Player { get; private set; }

    public void Setup(Player player)
    {
        Player = player;
        base.Setup(player);
    }

    protected override void OnCardAdded(CardCollection _, Card cardState, int index)
    {
        base.OnCardAdded(_, cardState, index);
        updateHandVisuals();
    }

    protected override void OnCardRemoved(CardCollection _, Card card, int index)
    {
        base.OnCardRemoved(_, card, index);
        updateHandVisuals();
    }

    private void updateHandVisuals()
    {
        int cardCount = CardDisplays.Count;
        for (int i = 0; i < cardCount; i++)
        {
            // Rotate in a fan shaped cone
            float rotationAngle = fanSpreadFactor * (i - (cardCount - 1) / 2f);
            CardDisplays[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = cardHorizontalSpacing * (i - (cardCount - 1) / 2f);

            // Normalize position between -1 to 1, for verticalOffset calculation
            float normalizedPosition = (cardCount != 1) ? 2f * i / (cardCount - 1) - 1f : 0f;

            // Spacing vertically according to the shape of -x^2's graph
            float verticalOffset = cardVerticalSpacing * -1 * (normalizedPosition * normalizedPosition);
            CardDisplays[i].transform.localPosition = new Vector2(horizontalOffset, verticalOffset);
        }
    }
}
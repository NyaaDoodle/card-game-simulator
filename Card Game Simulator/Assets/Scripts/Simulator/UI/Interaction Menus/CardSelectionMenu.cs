using UnityEngine.UI;

public class CardSelectionMenu : InteractionMenu
{
    public Button PlaceCardButton;

    private CardCollection cardCollection;

    public void Setup(CardCollection cardCollection)
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
        this.cardCollection = cardCollection;
        setupButtons();
    }

    private void setupButtons()
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
        PlaceCardButton.gameObject.SetActive(isAbleToPlaceCard());
    }

    private bool isAbleToPlaceCard()
    {
        return !cardCollection.IsEmpty;
    }
}

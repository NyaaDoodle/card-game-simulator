using UnityEngine.UI;

public class CardSelectionMenu : InteractionMenu
{
    public Button PlaceCardButton;

    private CardCollection cardCollection;

    public void Setup(CardCollection cardCollection)
    {
        this.cardCollection = cardCollection;
        setupButtons();
    }

    private void setupButtons()
    {
        PlaceCardButton.gameObject.SetActive(isAbleToPlaceCard());
    }

    private bool isAbleToPlaceCard()
    {
        return !cardCollection.IsEmpty;
    }
}

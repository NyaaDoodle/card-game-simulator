using UnityEngine.UI;

public class SpaceMenu : InteractionMenu
{
    public Button DrawCardButton;
    public Button FlipCardButton;
    public Button TakeAllCardsButton;
    public Button TransferCardsButton;

    private Space space;

    public void Setup(Space space)
    {
        this.space = space;
        setupButtons();
    }

    private void setupButtons()
    {
        DrawCardButton.gameObject.SetActive(isAbleToDrawCard());
        FlipCardButton.gameObject.SetActive(isAbleToFlipCard());
        TakeAllCardsButton.gameObject.SetActive(isAbleToTakeAllCards());
        TransferCardsButton.gameObject.SetActive(isAbleToTransferCards());
    }

    private bool isAbleToDrawCard()
    {
        return !space.IsEmpty;
    }

    private bool isAbleToFlipCard()
    {
        return !space.IsEmpty;
    }

    private bool isAbleToTakeAllCards()
    {
        return !space.IsEmpty;
    }

    private bool isAbleToTransferCards()
    {
        return !space.IsEmpty;
    }
}

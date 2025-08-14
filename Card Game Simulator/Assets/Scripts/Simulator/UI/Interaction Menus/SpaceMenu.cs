using UnityEngine.UI;

public class SpaceMenu : InteractionMenu
{
    public Button DrawCardButton;
    public Button FlipCardButton;

    private Space space;

    public void Setup(Space space)
    {
        LoggerReferences.Instance.InteractionMenuLogger.LogMethod();
        this.space = space;
        setupButtons();
    }

    private void setupButtons()
    {
        LoggerReferences.Instance.InteractionMenuLogger.LogMethod();
        DrawCardButton.gameObject.SetActive(isAbleToDrawCard());
        FlipCardButton.gameObject.SetActive(isAbleToFlipCard());
    }

    private bool isAbleToDrawCard()
    {
        return !space.IsEmpty;
    }

    private bool isAbleToFlipCard()
    {
        return !space.IsEmpty;
    }
}

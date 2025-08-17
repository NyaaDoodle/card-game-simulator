using UnityEngine.UI;

public class SpaceMenu : InteractionMenu
{
    public Button DrawCardButton;
    public Button FlipCardButton;

    private Space space;

    public void Setup(Space space)
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
        this.space = space;
        setupButtons();
    }

    private void setupButtons()
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
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

using UnityEngine.UI;

public class SpaceMenu : InteractionMenu
{
    public Button DrawCardButton;
    public Button FlipCardButton;

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

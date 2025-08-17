using UnityEngine.UI;

public class DeckMenu : InteractionMenu
{
    public Button DrawCardButton;
    public Button FlipCardButton;
    public Button ShuffleDeckButton;
    public Button SearchDeckButton;

    private Deck deck;

    public void Setup(Deck deck)
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
        this.deck = deck;
        setupButtons();
    }

    private void setupButtons()
    {
        LoggingManager.Instance.InteractionMenuLogger.LogMethod();
        DrawCardButton.gameObject.SetActive(isAbleToDrawCard());
        FlipCardButton.gameObject.SetActive(isAbleToFlipCard());
        ShuffleDeckButton.gameObject.SetActive(isAbleToShuffleDeck());
        SearchDeckButton.gameObject.SetActive(isAbleToSearchDeck());
    }

    private bool isAbleToDrawCard()
    {
        return !deck.IsEmpty;
    }

    private bool isAbleToFlipCard()
    {
        return !deck.IsEmpty;
    }

    private bool isAbleToShuffleDeck()
    {
        return deck.Cards.Count > 1;
    }

    private bool isAbleToSearchDeck()
    {
        return deck.Cards.Count > 1;
    }
}

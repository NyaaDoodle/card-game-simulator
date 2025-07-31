using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDisplay : StackableDisplay
{
    public Deck DeckState { get; private set; }

    public void Setup(Deck deckState)
    {
        base.Setup(deckState);
        DeckState = deckState;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        if (DeckState == null) return;
        Debug.Log($"Deck {DeckState.DeckData.Id} clicked");
    }
}
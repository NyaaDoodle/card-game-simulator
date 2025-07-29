using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDisplay : StackableDisplay
{
    public DeckState DeckState { get; private set; }

    public void Setup(DeckState deckState)
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
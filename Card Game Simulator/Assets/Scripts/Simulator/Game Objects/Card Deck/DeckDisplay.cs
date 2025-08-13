using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckDisplay : StackableDisplay
{
    protected Deck deck;

    protected override void SetCardCollection()
    {
        deck = GetComponent<Deck>();
        stackable = deck;
        cardCollection = deck;
    }
}
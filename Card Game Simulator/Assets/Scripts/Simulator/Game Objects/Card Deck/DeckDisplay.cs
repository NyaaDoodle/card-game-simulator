using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckDisplay : StackableDisplay
{
    public Deck Deck => (Deck)CardCollection;

    protected override void SetCardCollection()
    {
        LoggerReferences.Instance.DeckDisplayLogger.LogMethod();
        CardCollection = GetComponent<Deck>();
    }
}
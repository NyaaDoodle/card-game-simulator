using System;

public class Stackable : CardCollection
{
    public IStackableData StackableData { get; private set; }
    public Card TopCard => FirstCard;

    // Events
    public event Action<Stackable> CardsShuffled;
    public event Action<Stackable> StackableSelected;

    public virtual void Setup(IStackableData stackableData)
    {
        StackableData = stackableData;
    }

    public override void AddCardAtStart(Card card)
    {
        card = card.FaceDown();
        base.AddCardAtStart(card);
    }

    public override void AddCardAtEnd(Card card)
    {
        card = card.FaceDown();
        base.AddCardAtEnd(card);
    }

    public void Shuffle()
    {
        if (Cards.Count <= 1) return;

        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
        }

        OnCardsShuffled();
    }

    protected virtual void OnCardsShuffled()
    {
        CardsShuffled?.Invoke(this);
    }

    protected virtual void OnStackableSelected()
    {
        StackableSelected?.Invoke(this);
    }

    public void NotifySelection()
    {
        OnStackableSelected();
    }
}

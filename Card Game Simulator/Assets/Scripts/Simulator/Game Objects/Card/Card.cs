using System;

public class Card
{
    public CardData CardData { get; private set; }
    public bool IsFaceUp { get; private set; }

    // Events
    public event Action<Card> Flipped;
    public event Action<Card> Selected;

    public Card(CardData cardData)
    {
        CardData = cardData;
        IsFaceUp = false;
    }

    public void Flip()
    {
        IsFaceUp = !IsFaceUp;
        OnFlipped();
    }

    public void FlipFaceUp()
    {
        if (!IsFaceUp)
        {
            Flip();
        }
    }

    public void FlipFaceDown()
    {
        if (IsFaceUp)
        {
            Flip();
        }
    }

    public void NotifySelection()
    {
        OnSelected();
    }

    protected virtual void OnFlipped()
    {
        Flipped?.Invoke(this);
    }

    protected virtual void OnSelected()
    {
        Selected?.Invoke(this);
    }
}

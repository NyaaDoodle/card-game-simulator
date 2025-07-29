using System;

public class CardState
{
    public CardData CardData { get; private set; }
    public bool IsFaceUp { get; private set; }

    // Events
    public event Action<CardState> Flipped;

    public CardState(CardData cardData)
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

    protected virtual void OnFlipped()
    {
        Flipped?.Invoke(this);
    }
}

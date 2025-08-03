using System;

public class SelectionItem
{
    public CardCollection SelectedCardCollection { get; set; } = null;
    public Card SelectedCard { get; set; } = null;

    public bool IsEmpty => SelectedCardCollection == null && SelectedCard == null;

    public void SetEmpty()
    {
        SelectedCardCollection = null;
        SelectedCard = null;
    }
}
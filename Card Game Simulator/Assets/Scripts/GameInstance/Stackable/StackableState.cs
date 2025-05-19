using System;
using System.Collections.Generic;
using UnityEngine;

public class StackableState : MonoBehaviour
{
    public LinkedList<GameObject> Cards { get; private set; } = new LinkedList<GameObject>();
    public GameObject TopCard => Cards.First?.Value;

    public event Action<StackableState> CardsChanged;

    public void AddCards(ICollection<GameObject> cardsToAdd)
    {
        foreach (GameObject card in cardsToAdd)
        {
            AddCardToBottom(card);
        }
    }

    public void AddCardToTop(GameObject card)
    {
        Cards.AddFirst(card);
        attachCardTransform(card);
        OnCardsChanged();
    }

    public void AddCardToBottom(GameObject card)
    {
        Cards.AddLast(card);
        attachCardTransform(card);
        OnCardsChanged();
    }

    public GameObject DrawCard()
    {
        if (Cards.Count <= 0) return null;
        GameObject cardDrawn = Cards.First.Value;
        detachCardTransform(cardDrawn);
        Cards.RemoveFirst();
        OnCardsChanged();

        return cardDrawn;
    }

    protected virtual void OnCardsChanged()
    {
        CardsChanged?.Invoke(this);
    }

    private void attachCardTransform(GameObject card)
    {
        card.transform.parent = this.gameObject.transform;
        card.transform.localPosition = Vector3.zero;
    }

    private void detachCardTransform(GameObject card)
    {
        card.transform.parent = null;
    }
}

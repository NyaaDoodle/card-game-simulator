using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class StackableState : MonoBehaviour
{
    public LinkedList<GameObject> Cards { get; } = new LinkedList<GameObject>();
    public GameObject TopCard => Cards.First?.Value;
    public int CardCount => Cards.Count;
    public bool IsInteractable { get; private set; } = true;

    // Stackable State Events
    public event Action<StackableState, GameObject> CardAdded;
    public event Action<StackableState, GameObject> CardRemoved;
    public event Action<StackableState> CardsShuffled;
    public event Action<StackableState> ChangedIsInteractable;

    private RectTransform stackableRectTransform;

    void Awake()
    {
        stackableRectTransform = GetComponent<RectTransform>();
    }

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
        OnCardAdded(card);
    }

    public void AddCardToBottom(GameObject card)
    {
        Cards.AddLast(card);
        attachCardTransform(card);
        OnCardAdded(card);
    }

    public GameObject DrawCard()
    {
        if (Cards.Count <= 0) return null;
        GameObject cardDrawn = Cards.First.Value;
        detachCardTransform(cardDrawn);
        Cards.RemoveFirst();
        OnCardRemoved(cardDrawn);

        return cardDrawn;
    }

    public void FlipTopCard()
    {
        if (Cards.Count <= 0) return;
        TopCard.GetComponent<CardState>().Flip();
    }

    public bool RemoveCard(GameObject card)
    {
        if (Cards.Remove(card))
        {
            detachCardTransform(card);
            OnCardRemoved(card);
            return true;
        }
        return false;
    }

    public void ShuffleCards()
    {
        if (Cards.Count <= 1) return;

        // Shuffle as an array
        GameObject[] cardArray = new GameObject[Cards.Count];
        Cards.CopyTo(cardArray, 0);

        for (int i = cardArray.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (cardArray[i], cardArray[randomIndex]) = (cardArray[randomIndex], cardArray[i]);
        }

        // Rebuild the linked list from the array
        Cards.Clear();
        foreach (GameObject card in cardArray)
        {
            Cards.AddLast(card);
        }

        OnCardsShuffled();
    }

    public void SetInteractable(bool interactable)
    {
        IsInteractable = interactable;
        OnChangedIsInteractable();
    }

    protected virtual void OnCardAdded(GameObject cardAdded)
    {
        CardAdded?.Invoke(this, cardAdded);
    }

    protected virtual void OnCardRemoved(GameObject cardRemoved)
    {
        CardRemoved?.Invoke(this, cardRemoved);
    }

    protected virtual void OnCardsShuffled()
    {
        CardsShuffled?.Invoke(this);
    }

    protected virtual void OnChangedIsInteractable()
    {
        ChangedIsInteractable?.Invoke(this);
    }

    private void attachCardTransform(GameObject card)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        cardRectTransform.SetParent(stackableRectTransform, false);

        cardRectTransform.anchoredPosition = Vector2.zero;
        cardRectTransform.localScale = Vector3.one;
        cardRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        cardRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        cardRectTransform.pivot = new Vector2(0.5f, 0.5f);

        setChildIndexOnStackable(cardRectTransform);
    }

    private void setChildIndexOnStackable(RectTransform cardRectTransform)
    {
        int targetIndex = 1;
        cardRectTransform.SetSiblingIndex(targetIndex);
        placeInteractionButtonAtLastIndex();
    }

    private void placeInteractionButtonAtLastIndex()
    {
        StackableInteraction stackableInteraction = GetComponent<StackableInteraction>();
        if (stackableInteraction != null)
        {
            stackableInteraction.PutInteractionButtonAtLastOfObjectChildren();
        }
    }

    private void detachCardTransform(GameObject card)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        Vector3 worldPosition = cardRectTransform.position;

        // Assuming the stackables are spawned on the Table View container
        RectTransform tableViewContainer = GetComponentInParent<RectTransform>();
        if (tableViewContainer != null)
        {
            cardRectTransform.SetParent(tableViewContainer, true);
            cardRectTransform.position = worldPosition;
        }
        else
        {
            cardRectTransform.SetParent(null, true);
        }
    }

    //private void reorderCardChildren()
    //{
    //    GameObject[] cardsArray = new GameObject[CardCount];
    //    Cards.CopyTo(cardsArray, 0);
    //    for (int i = 0; i < cardsArray.Length; i++)
    //    {
    //        cardsArray[i].transform.SetSiblingIndex(i);
    //    }
    //}
}

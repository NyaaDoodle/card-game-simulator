using System.Collections.Generic;
using UnityEngine;

public class PlayerHandState : MonoBehaviour
{
    // TODO fan spread is not good at around 10 cards
    [Header("Container")]
    [SerializeField] private Transform cardsContainer;

    [Header("Cards in Hand Display Settings")]
    [SerializeField] private float fanSpreadFactor = -10f;
    [SerializeField] private float cardHorizontalSpacing = 150;
    [SerializeField] private float cardVerticalSpacing = 100;

    private readonly List<GameObject> cardsInHand = new List<GameObject>();
    private GameObject currentSelectedCard;

    public void AddCardToHand(GameObject card)
    {
        cardsInHand.Add(card);
        CardUtilities.AttachCardTransformAndCenter(card, cardsContainer);
        CardState cardState = card.GetComponent<CardState>();
        cardState.ShowCard();
        cardState.FlipFaceUp();
        updateHandVisuals();
    }

    public GameObject RemoveSelectedCard()
    {
        if (cardsInHand.Remove(currentSelectedCard))
        {
            resetCardRotation(currentSelectedCard);
            currentSelectedCard.GetComponent<CardState>().HideCard();

            // Detach card
            CardUtilities.AttachCardTransformAndCenter(currentSelectedCard, transform.parent);

            updateHandVisuals();

            return currentSelectedCard;
        }
        else
        {
            return null;
        }
    }

    void Update()
    {
        updateHandVisuals();
    }

    private void updateHandVisuals()
    {
        int cardsInHandCount = cardsInHand.Count;
        for (int i = 0; i < cardsInHandCount; i++)
        {
            // Rotate in a fan shaped cone
            float rotationAngle = fanSpreadFactor * (i - (cardsInHandCount - 1) / 2f);
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = cardHorizontalSpacing * (i - (cardsInHandCount - 1) / 2f);

            // Normalize position between -1 to 1, for verticalOffset calculation
            float normalizedPosition = (cardsInHandCount != 1) ? 2f * i / (cardsInHandCount - 1) - 1f : 0f;

            // Spacing vertically according to the shape of -x^2's graph
            float verticalOffset = cardVerticalSpacing * -1 * (normalizedPosition * normalizedPosition);
            cardsInHand[i].transform.localPosition = new Vector2(horizontalOffset, verticalOffset);
        }
    }

    private void resetCardRotation(GameObject card)
    {
        card.transform.localRotation = Quaternion.identity;
    }
}

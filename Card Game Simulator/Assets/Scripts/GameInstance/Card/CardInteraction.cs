using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
public class CardInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject cardButtonObject;
    private CardState cardState;

    public event Action<CardInteraction> OnCardClicked;

    void Awake()
    {
        cardState = GetComponent<CardState>();
    }

    void Start()
    {
        setupInteraction();
    }

    private void setupInteraction()
    {
        Button cardButton = cardButtonObject.GetComponent<Button>();
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(HandleCardClick);
        }

        //if (frontSideImage != null)
        //    frontSideImage.raycastTarget = true;
        //if (backSideImage != null)
        //    backSideImage.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardState.IsInteractable)
        {
            HandleCardClick();
        }
    }

    private void HandleCardClick()
    {
        Debug.Log($"Card clicked: {cardState.Name}");
        OnCardClicked?.Invoke(this);
    }
}

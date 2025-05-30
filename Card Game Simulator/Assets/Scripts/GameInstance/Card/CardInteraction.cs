using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
public class CardInteraction : MonoBehaviour
{
    [SerializeField] private Button cardButton;
    private CardState cardState;

    public event Action<CardInteraction> OnCardClicked;

    void Awake()
    {
        cardState = GetComponent<CardState>();
    }

    void Start()
    {
        cardState.ChangedIsInteractable += onChangedIsInteractable;
        setupInteraction();
    }

    private void setupInteraction()
    {
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(HandleCardClick);
        }
    }

    private void HandleCardClick()
    {
        OnCardClicked?.Invoke(this);
    }

    private void onChangedIsInteractable(CardState _)
    {
        if (cardButton != null)
        {
            cardButton.interactable = cardState.IsInteractable;
        }
    }
}

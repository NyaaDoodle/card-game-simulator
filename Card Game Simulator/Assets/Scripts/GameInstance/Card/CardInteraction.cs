using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
public class CardInteraction : MonoBehaviour
{
    [SerializeField] private Button interactionButton;
    private CardState cardState;

    public event Action<CardState> Click;

    void Awake()
    {
        cardState = GetComponent<CardState>();
    }

    void Start()
    {
        cardState.ChangedIsInteractable += onChangedIsInteractable;
        setupInteractionButton();
    }

    void OnDestroy()
    {
        cardState.ChangedIsInteractable -= onChangedIsInteractable;
    }

    private void setupInteractionButton()
    {
        if (interactionButton != null)
        {
            interactionButton.onClick.AddListener(handleClick);
        }
    }

    private void handleClick()
    {
        Debug.Log("Card Clicked!");
        Click?.Invoke(cardState);
    }

    private void onChangedIsInteractable(CardState _)
    {
        if (interactionButton != null)
        {
            interactionButton.interactable = cardState.IsInteractable;
        }
    }
}

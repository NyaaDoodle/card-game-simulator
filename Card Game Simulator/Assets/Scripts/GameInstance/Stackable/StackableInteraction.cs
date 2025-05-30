using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StackableState))]
public class StackableInteraction : MonoBehaviour
{
    [SerializeField] private Button interactionButton;
    private StackableState stackableState;

    public event Action<StackableInteraction> OnStackableClick;

    void Awake()
    {
        stackableState = GetComponent<StackableState>();
    }

    void Start()
    {
        stackableState.ChangedIsInteractable += onChangedIsInteractable;
        setupInteraction();
    }

    public void PutInteractionButtonAtLastOfObjectChildren()
    {
        interactionButton.transform.SetAsLastSibling();
    }

    private void setupInteraction()
    {
        if (interactionButton != null)
        {
            interactionButton.onClick.AddListener(handleClick);
        }
    }

    private void handleClick()
    {
        OnStackableClick?.Invoke(this);
    }

    private void onChangedIsInteractable(StackableState _)
    {
        if (interactionButton != null)
        {
            interactionButton.interactable = stackableState.IsInteractable;
        }
    }
}

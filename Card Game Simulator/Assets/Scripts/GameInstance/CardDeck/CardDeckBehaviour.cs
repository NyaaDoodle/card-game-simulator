using UnityEngine;

[RequireComponent(typeof(StackableInteraction))]
public class CardDeckBehaviour : MonoBehaviour
{
    private DeckData deckData;
    private SelectionManager selectionManager;

    void Awake()
    {
        StackableInteraction stackableInteraction = GetComponent<StackableInteraction>();
        selectionManager = FindFirstObjectByType<SelectionManager>();
        stackableInteraction.OnStackableClick += onStackableClick;
    }

    private void onStackableClick(StackableInteraction _)
    {
        if (selectionManager != null)
        {
            selectionManager.SelectObject(gameObject, SelectionType.Deck);
        }
    }

    public void Initialize(DeckData data)
    {
        deckData = data;
    }
}

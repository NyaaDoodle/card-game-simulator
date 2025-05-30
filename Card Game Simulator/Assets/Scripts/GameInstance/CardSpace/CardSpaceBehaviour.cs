using UnityEngine;

public class CardSpaceBehaviour : MonoBehaviour
{
    private SpaceData spaceData;
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
            selectionManager.SelectObject(gameObject, SelectionType.Space);
        }
    }

    public void Initialize(SpaceData data)
    {
        spaceData = data;
    }
}

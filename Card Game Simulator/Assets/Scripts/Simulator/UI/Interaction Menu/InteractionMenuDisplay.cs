using UnityEngine;

public class InteractionMenuDisplay : MonoBehaviour
{
    [SerializeField] private GameObject menuItemButtonPrefab;
    [SerializeField] private RectTransform menuItemContainer;
    public InteractionMenu InteractionMenu { get; private set; }

    public void Setup(InteractionMenu interactionMenu)
    {
        InteractionMenu = interactionMenu;
        spawnMenuItems();
    }

    private void spawnMenuItems()
    {
        foreach (InteractionMenuItem menuItem in InteractionMenu.MenuItems)
        {
            spawnMenuItem(menuItem);
        }
    }

    private void spawnMenuItem(InteractionMenuItem menuItem)
    {

    }
}

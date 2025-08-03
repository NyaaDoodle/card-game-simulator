using UnityEngine;
using UnityEngine.UI;

public class InteractionMenuTest : MonoBehaviour
{
    [SerializeField] private InteractionMenuManager uiManager;
    [SerializeField] private Button showMenuButton;
    void Start()
    {
        showMenuButton.onClick.AddListener(uiManager.ShowInstanceMenu);
    }
}

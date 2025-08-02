using UnityEngine;
using UnityEngine.UI;

public class InteractionMenuTest : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Button showMenuButton;
    void Start()
    {
        showMenuButton.onClick.AddListener(uiManager.ShowInstanceMenu);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HamburgerMenuButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InteractionMenuManager interactionMenuManager;
    
    void Start()
    {
        button.onClick.AddListener(interactionMenuManager.ShowInstanceMenu);
    }
}

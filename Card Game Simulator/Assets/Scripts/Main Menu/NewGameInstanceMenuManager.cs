using System;
using UnityEngine;

public class NewGameInstanceMenuManager : MonoBehaviour
{
    [SerializeField] private GameTemplateSelectionGrid gameTemplateSelectionGrid;
    
    private void OnEnable()
    {
        gameTemplateSelectionGrid.Show(
            (gameTemplate) =>
                {
                    SimulatorNetworkManager.singleton.HostGame(gameTemplate);
                },
            null);
    }

    private void OnDisable()
    {
        gameTemplateSelectionGrid.Hide();
    }
}

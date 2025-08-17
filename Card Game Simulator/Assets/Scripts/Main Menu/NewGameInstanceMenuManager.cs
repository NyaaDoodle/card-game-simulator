using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameInstanceMenuManager : MonoBehaviour
{
    public void StartTestGameTemplateInstance()
    {
        SimulatorNetworkManager.singleton.HostGame();
    }
}

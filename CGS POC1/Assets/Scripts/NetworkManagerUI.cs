using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private GameObject StaticGameObjectsForTesting;

    private void Awake()
    {
        startHostButton.onClick.AddListener(
            () =>
                {
                    NetworkManager.Singleton.StartHost();
                    doAfterNetworkStart();
                });
        startServerButton.onClick.AddListener(
            () =>
                {
                    NetworkManager.Singleton.StartServer();
                    doAfterNetworkStart();
                });
        startClientButton.onClick.AddListener(
            () =>
                {
                    NetworkManager.Singleton.StartClient();
                    doAfterNetworkStart();
                });
    }

    private void doAfterNetworkStart()
    {
        activateGame();
        hideUI();
    }

    private void activateGame()
    {
        StaticGameObjectsForTesting.SetActive(true);
    }

    private void hideUI()
    {
        gameObject.SetActive(false);
    }
}

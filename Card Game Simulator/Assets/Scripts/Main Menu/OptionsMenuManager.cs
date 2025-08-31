using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_InputField lanBroadcastAddressInputField;
    [SerializeField] private TMP_InputField cloudBackendIpInputField;
    [SerializeField] private TMP_InputField cloudBackendPortInputField;
    [SerializeField] private Toggle debugWindowToggle;
    
    private void OnEnable()
    {
        playerNameInputField.text = PlayerPrefsManager.Instance.PlayerName;
        lanBroadcastAddressInputField.text = PlayerPrefsManager.Instance.LANBroadcastAddress;
        cloudBackendIpInputField.text = PlayerPrefsManager.Instance.CloudBackendIP;
        cloudBackendPortInputField.text = PlayerPrefsManager.Instance.CloudBackendPort.ToString();
        debugWindowToggle.isOn = PlayerPrefsManager.Instance.DebugWindowToggle;
        playerNameInputField.onEndEdit.AddListener((text) => PlayerPrefsManager.Instance.PlayerName = text);
        lanBroadcastAddressInputField.onEndEdit.AddListener((text) =>
            {
                SimulatorNetworkManager.singleton.SimulatorNetworkDiscovery.BroadcastAddress = text;
                PlayerPrefsManager.Instance.LANBroadcastAddress = text;
            });
        cloudBackendIpInputField.onEndEdit.AddListener((text) =>
            {
                ContentServerAPIManager.Instance.CloudBackendServerIp = text;
                PlayerPrefsManager.Instance.CloudBackendIP = text;
            });
        cloudBackendPortInputField.onEndEdit.AddListener((text) =>
            {
                bool success = int.TryParse(text, out int portNumber);
                if (success && portNumber >= 1 && portNumber <= 65535)
                {
                    ContentServerAPIManager.Instance.CloudBackendServerPort = portNumber;
                    PlayerPrefsManager.Instance.CloudBackendPort = portNumber;
                }
            });
        debugWindowToggle.onValueChanged.AddListener((isOn) =>
            {
                InGameDebugWindowManager.Instance.IsActive = isOn;
                PlayerPrefsManager.Instance.DebugWindowToggle = isOn;
            });
    }

    private void OnDisable()
    {
        lanBroadcastAddressInputField.onEndEdit.RemoveAllListeners();
        cloudBackendIpInputField.onEndEdit.RemoveAllListeners();
        cloudBackendPortInputField.onEndEdit.RemoveAllListeners();
        debugWindowToggle.onValueChanged.RemoveAllListeners();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField lanBroadcastAddressInputField;
    [SerializeField] private TMP_InputField cloudBackendIp;
    [SerializeField] private TMP_InputField cloudBackendPort;
    [SerializeField] private Toggle debugWindowToggle;
    
    private void OnEnable()
    {
        lanBroadcastAddressInputField.text = SimulatorNetworkManager.singleton.NetworkDiscovery.BroadcastAddress;
        cloudBackendIp.text = ContentServerAPIManager.Instance.CloudBackendServerIp;
        cloudBackendPort.text = ContentServerAPIManager.Instance.CloudBackendServerPort.ToString();
        debugWindowToggle.isOn = InGameDebugWindowManager.Instance.IsActive;
        lanBroadcastAddressInputField.onEndEdit.AddListener((text) =>
            {
                SimulatorNetworkManager.singleton.NetworkDiscovery.BroadcastAddress = text;
            });
        cloudBackendIp.onEndEdit.AddListener((text) =>
            {
                ContentServerAPIManager.Instance.CloudBackendServerIp = text;
            });
        cloudBackendPort.onEndEdit.AddListener((text) =>
            {
                bool success = int.TryParse(text, out int portNumber);
                if (success && portNumber >= 0 && portNumber <= 65535)
                {
                    ContentServerAPIManager.Instance.CloudBackendServerPort = portNumber;
                }
            });
        debugWindowToggle.onValueChanged.AddListener((isOn) =>
            {
                InGameDebugWindowManager.Instance.IsActive = isOn;
            });
    }

    private void OnDisable()
    {
        lanBroadcastAddressInputField.onEndEdit.RemoveAllListeners();
        cloudBackendIp.onEndEdit.RemoveAllListeners();
        cloudBackendPort.onEndEdit.RemoveAllListeners();
        debugWindowToggle.onValueChanged.RemoveAllListeners();
    }
}

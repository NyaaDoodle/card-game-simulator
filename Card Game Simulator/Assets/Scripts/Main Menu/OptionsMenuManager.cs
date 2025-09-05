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
    [SerializeField] private TMP_InputField localContentServerPortInputField;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private GameObject fullScreenToggleParent;
    
    private void OnEnable()
    {
        playerNameInputField.text = PlayerPrefsManager.Instance.PlayerName;
        lanBroadcastAddressInputField.text = PlayerPrefsManager.Instance.LANBroadcastAddress;
        cloudBackendIpInputField.text = PlayerPrefsManager.Instance.CloudBackendIP;
        cloudBackendPortInputField.text = PlayerPrefsManager.Instance.CloudBackendPort.ToString();
        debugWindowToggle.isOn = PlayerPrefsManager.Instance.DebugWindowToggle;
        localContentServerPortInputField.text = PlayerPrefsManager.Instance.LocalContentServerPort.ToString();
        playerNameInputField.onEndEdit.AddListener((text) => PlayerPrefsManager.Instance.PlayerName = text);
        lanBroadcastAddressInputField.onEndEdit.AddListener((text) =>
            {
                SimulatorNetworkManager.singleton.SimulatorNetworkDiscovery.BroadcastAddress = text;
                PlayerPrefsManager.Instance.LANBroadcastAddress = text;
            });
        cloudBackendIpInputField.onEndEdit.AddListener((text) =>
            {
                PlayerPrefsManager.Instance.CloudBackendIP = text;
            });
        cloudBackendPortInputField.onEndEdit.AddListener((text) =>
            {
                bool success = int.TryParse(text, out int portNumber);
                if (success && portNumber >= 1 && portNumber <= 65535)
                {
                    PlayerPrefsManager.Instance.CloudBackendPort = portNumber;
                }
            });
        debugWindowToggle.onValueChanged.AddListener((isOn) =>
            {
                InGameDebugWindowManager.Instance.IsActive = isOn;
                PlayerPrefsManager.Instance.DebugWindowToggle = isOn;
            });
        localContentServerPortInputField.onEndEdit.AddListener((text) =>
            {
                bool success = int.TryParse(text, out int portNumber);
                if (success && portNumber >= 1 && portNumber <= 65535)
                {
                    PlayerPrefsManager.Instance.LocalContentServerPort = portNumber;
                }
            });
        #if !UNITY_IOS && !UNITY_ANDROID
        fullScreenToggleParent.SetActive(true);
        fullScreenToggle.onValueChanged.AddListener((isOn) =>
            {
                Screen.fullScreen = isOn;
                PlayerPrefsManager.Instance.IsFullscreen = isOn;
            });
        #else
        fullScreenToggleParent.SetActive(false);
        #endif
    }

    private void OnDisable()
    {
        lanBroadcastAddressInputField.onEndEdit.RemoveAllListeners();
        cloudBackendIpInputField.onEndEdit.RemoveAllListeners();
        cloudBackendPortInputField.onEndEdit.RemoveAllListeners();
        debugWindowToggle.onValueChanged.RemoveAllListeners();
        localContentServerPortInputField.onEndEdit.RemoveAllListeners();
        fullScreenToggle.onValueChanged.RemoveAllListeners();
    }
}

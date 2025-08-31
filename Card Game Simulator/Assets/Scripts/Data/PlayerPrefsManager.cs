using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager Instance { get; private set; }

    [SerializeField] private string defaultPlayerName;
    [SerializeField] private string defaultLANBroadcastAddress;
    [SerializeField] private string defaultCloudBackendIP;
    [SerializeField] private int defaultCloudBackendPort;
    [SerializeField] private bool defaultDebugWindowToggle;

    private const string playerNameKey = "PlayerName";
    private const string lanBroadcastAddressKey = "LANBroadcastAddress";
    private const string cloudBackendIPKey = "CloudBackendIP";
    private const string cloudBackendPortKey = "CloudBackendPort";
    private const string debugWindowToggleKey = "DebugWindowToggle";
    
    public bool IsReady { get; private set; }

    public string PlayerName
    {
        get => PlayerPrefs.GetString(playerNameKey, defaultPlayerName);
        set
        {
            PlayerPrefs.SetString(playerNameKey, value);
            PlayerPrefs.Save();
        }
    }
    public string LANBroadcastAddress
    {
        get => PlayerPrefs.GetString(lanBroadcastAddressKey, defaultLANBroadcastAddress);
        set
        {
            PlayerPrefs.SetString(lanBroadcastAddressKey, value);
            PlayerPrefs.Save();
        }
    }

    public string CloudBackendIP
    {
        get => PlayerPrefs.GetString(cloudBackendIPKey, defaultCloudBackendIP);
        set
        {
            PlayerPrefs.SetString(cloudBackendIPKey, value);
            PlayerPrefs.Save();
        }
    }

    public int CloudBackendPort
    {
        get => PlayerPrefs.GetInt(cloudBackendPortKey, defaultCloudBackendPort);
        set
        {
            PlayerPrefs.SetInt(cloudBackendPortKey, value);
            PlayerPrefs.Save();
        }
    }

    public bool DebugWindowToggle
    {
        get => PlayerPrefs.GetInt(debugWindowToggleKey, (defaultDebugWindowToggle ? 1 : 0)) > 0;
        set
        {
            int intValue = value ? 1 : 0;
            PlayerPrefs.SetInt(debugWindowToggleKey, intValue);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        initializeInstance();
        onReady();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void onReady()
    {
        IsReady = true;
    }
}

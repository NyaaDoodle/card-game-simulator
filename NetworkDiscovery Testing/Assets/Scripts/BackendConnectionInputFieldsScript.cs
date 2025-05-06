using Mirror;
using TMPro;
using UnityEngine;

public class BackendConnectionInputFieldsScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipInputField;
    [SerializeField] private TMP_InputField portInputField;
    [SerializeField] private TMP_InputField uriInputField;
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private DummyDataSpawner dummyDataSpawner;

    [SerializeField] private string defaultIp = "localhost";
    [SerializeField] private int defaultPort = 8080;
    [SerializeField] private string defaultFetchUri = "/templates";
    [SerializeField] private int defaultId = 1;

    public string IpText => ipInputField.text;
    public string PortText => portInputField.text;
    public string UriText => uriInputField.text;
    public string IdText => idInputField.text;

    void Start()
    {
        initializeInputFieldValues();
    }

    private void initializeInputFieldValues()
    {
        ipInputField.text = defaultIp;
        portInputField.text = defaultPort.ToString();
        uriInputField.text = defaultFetchUri;
        idInputField.text = defaultId.ToString();
    }
}

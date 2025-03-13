using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class MessageBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject messageUI = null;
    [SerializeField] private TMP_Text messageText = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private Button sendButton = null;

    public override void OnStartAuthority()
    {
        messageUI.SetActive(true);
    }

    [ClientCallback]
    private void OnDestroy()
    {
        
    }
}

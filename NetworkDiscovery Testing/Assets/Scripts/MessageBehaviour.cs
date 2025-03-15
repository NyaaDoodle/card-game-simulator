using UnityEngine;
using Mirror;
using TMPro;
using System;

public class MessageBehaviour : NetworkBehaviour
{
    [SerializeField] private TMP_Text messageText = null;
    [SerializeField] private TMP_InputField inputField = null;

    private static event Action<string> onMessage;

    public override void OnStartLocalPlayer()
    {
        this.gameObject.SetActive(true);
        onMessage += handleNewMessage;
    }

    [ClientCallback]
    void OnDestroy()
    {
        if (!isOwned) { return; }
        onMessage -= handleNewMessage;
    }

    [Client]
    public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }

        commandSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command]
    private void commandSendMessage(string message)
    {
        rpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void rpcHandleMessage(string message)
    {
        onMessage?.Invoke($"{Environment.NewLine}{message}");
    }

    private void handleNewMessage(string message)
    {
        messageText.text += message;
    }
}
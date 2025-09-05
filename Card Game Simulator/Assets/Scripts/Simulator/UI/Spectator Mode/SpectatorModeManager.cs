using UnityEngine;

public class SpectatorModeManager : MonoBehaviour
{
    public static SpectatorModeManager Instance { get; private set; }
    public bool IsActive { get; private set; }
    [SerializeField] private RectTransform blockingPanel;

    private void Awake()
    {
        initializeInstance();
        gameObject.SetActive(false);
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
        }
    }

    public void Activate()
    {
        IsActive = true;
        onToggleActive();
    }

    public void Deactivate()
    {
        IsActive = false;
        onToggleActive();
    }

    public void ToggleActive()
    {
        IsActive = !IsActive;
        onToggleActive();
    }

    private void onToggleActive()
    {
        blockingPanel.gameObject.SetActive(IsActive);
        PlayerHandDisplay playerHandDisplay = PlayerManager.Instance.LocalPlayerHandDisplay;
        if (playerHandDisplay != null)
        {
            playerHandDisplay.gameObject.SetActive(!IsActive);
        }
        PlayerManager.Instance.LocalPlayer.IsSpectating = IsActive;
        string statusText = IsActive ? "on" : "off";
        PopupMessageManager.NewPopupMessage($"Spectator mode {statusText}", 2f);
    }
}

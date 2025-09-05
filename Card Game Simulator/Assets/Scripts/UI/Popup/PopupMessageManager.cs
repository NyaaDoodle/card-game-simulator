using UnityEngine;

public static class PopupMessageManager
{
    public static Color DefaultTextColor { get; set; } = Color.black;
    
    public static void NewPopupMessage(string text, float durationInSeconds)
    {
        NewPopupMessage(text, durationInSeconds, DefaultTextColor);
    }
    
    public static void NewPopupMessage(string text, float durationInSeconds, Color textColor)
    {
        PopupMessage popupMessage = GameObject.Instantiate(PrefabReferences.Instance.PopupMessagePrefab)
            .GetComponent<PopupMessage>();
        popupMessage.gameObject.transform.SetAsLastSibling();
        popupMessage.Setup(text, durationInSeconds, textColor);
    }
}

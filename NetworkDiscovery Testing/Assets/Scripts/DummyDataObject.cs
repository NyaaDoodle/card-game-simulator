using Mirror;
using TMPro;
using UnityEngine;

public class DummyDataObject : NetworkBehaviour
{
    [SyncVar] private DummyData dummyData;
    private TMP_Text textComponent;

    public override void OnStartClient()
    {
        base.OnStartClient();
        textComponent = GetComponent<TMP_Text>();
        updateTextDisplay();
    }

    [Server]
    public void setDummyData(DummyData dummyDataValue)
    {
        dummyData = dummyDataValue;
    }

    public void Update()
    {
        updateTextDisplay();
    }

    private void updateTextDisplay()
    {
        if (dummyData != null && textComponent != null)
        {
            textComponent.text = dummyData.Info;
        }
    }
}

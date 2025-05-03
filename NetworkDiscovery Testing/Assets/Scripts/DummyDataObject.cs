using Mirror;
using TMPro;
using UnityEngine;

public class DummyDataObject : NetworkBehaviour
{
    [SyncVar(hook = nameof(onDummyDataChanged))]
    private bool isDummyDataLoaded = false;
    [SyncVar] private int id;
    [SyncVar] private string info;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text textComponent;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        updateDummyDataDisplay();
    }

    [Server]
    public void setDummyData(DummyData dummyDataValue)
    {
        if (!isDummyDataLoaded)
        {
            id = dummyDataValue.Id;
            info = dummyDataValue.Info;
            isDummyDataLoaded = true;
        }
        else
        {
            Debug.Log("DummyData has been loaded on DummyDataObject");
        }
    }

    private void onDummyDataChanged(bool oldValue, bool newValue)
    {
        updateDummyDataDisplay();
    }

    private void updateDummyDataDisplay()
    {
        if (isDummyDataLoaded)
        {
            updateIdColor();
            updateDisplayInfoText();
        }
    }

    private void updateIdColor()
    {
        switch (id)
        {
            case 1:
                spriteRenderer.color = Color.green;
                break;
            case 2:
                spriteRenderer.color = Color.red;
                break;
            default:
                spriteRenderer.color = Color.yellow;
                break;
        }
    }

    private void updateDisplayInfoText()
    {
        textComponent.text = info;
    }
}

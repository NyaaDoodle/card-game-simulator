using UnityEngine;

public class ContainerReferences : MonoBehaviour
{
    public static ContainerReferences Instance { get; private set; }

    public RectTransform TableContainer;
    public RectTransform TableObjectsContainer;
    public RectTransform PlayerHandContainer;
    public RectTransform InteractionMenuItemsContainer;

    void Awake()
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
}
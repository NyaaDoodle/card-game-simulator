using UnityEngine;

public class ManagerReferences : MonoBehaviour
{
    public static ManagerReferences Instance { get; private set; }

    public GameInstanceManager GameInstanceManager;
    public PlayerManager PlayerManager;
    public SelectionManager SelectionManager;

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
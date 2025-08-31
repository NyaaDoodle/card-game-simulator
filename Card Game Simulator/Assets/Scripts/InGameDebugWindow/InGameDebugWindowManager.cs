using UnityEngine;

public class InGameDebugWindowManager : MonoBehaviour
{
    public static InGameDebugWindowManager Instance { get; private set; }
    public bool IsReady { get; private set; }

    [SerializeField] private bool isActive;

    public bool IsActive
    {
        get => isActive;
        set
        {
            isActive = value;
            gameObject.SetActive(isActive);
        }
    }

    private void Awake()
    {
        initializeInstance();
    }

    private void Start()
    {
        IsActive = PlayerPrefsManager.Instance.DebugWindowToggle;
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

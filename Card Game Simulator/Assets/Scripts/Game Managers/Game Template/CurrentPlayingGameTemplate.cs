using UnityEngine;

public class CurrentPlayingGameTemplate : MonoBehaviour
{
    public static CurrentPlayingGameTemplate Instance { get; private set; }
    public GameTemplate GameTemplate;

    private void Awake()
    {
        initiateInstance();
    }

    private void initiateInstance()
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
}

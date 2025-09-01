using System;
using UnityEngine;

public class SimulatorGlobalData : MonoBehaviour
{
    public static SimulatorGlobalData Instance { get; private set; }
    public bool IsReady { get; private set; }
    public GameTemplate? CurrentPlayingGameTemplate { get; set; }
    public string ApplicationInstanceId { get; private set; }

    private void Awake()
    {
        initializeInstance();
        ApplicationInstanceId = Guid.NewGuid().ToString();
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

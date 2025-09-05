using UnityEngine;

public class CameraControlsManager : MonoBehaviour
{
    public static CameraControlsManager Instance { get; private set; }
    public SimulatorCameraControls SimulatorCameraControls { get; private set; }
    private bool isCameraControlsEnabled;

    public bool IsCameraControlsEnabled
    {
        get => isCameraControlsEnabled;
        set
        {
            isCameraControlsEnabled = value;
            if (isCameraControlsEnabled)
            {
                SimulatorCameraControls.Camera.Enable();
            }
            else
            {
                SimulatorCameraControls.Camera.Disable();
            }
        }
    }

    private void Awake()
    {
        initializeInstance();
        SimulatorCameraControls = new SimulatorCameraControls();
    }

    private void OnEnable()
    {
        IsCameraControlsEnabled = true;
    }

    private void OnDisable()
    {
        IsCameraControlsEnabled = false;
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
}
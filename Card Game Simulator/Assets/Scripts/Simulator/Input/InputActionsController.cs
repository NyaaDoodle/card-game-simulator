using System;
using UnityEngine;

public class InputActionsController : MonoBehaviour
{
    public static InputActionsController Instance { get; private set; }
    public GameInstanceCameraInputActions GameInstanceCameraInputActions { get; private set; }
    private bool isDragInputActionActive;

    public bool IsDragInputActionActive
    {
        get => isDragInputActionActive;
        set
        {
            isDragInputActionActive = value;
            if (isDragInputActionActive)
            {
                GameInstanceCameraInputActions.Camera.Drag.Enable();
            }
            else
            {
                GameInstanceCameraInputActions.Camera.Drag.Disable();
            }
        }
    }

    private void Awake()
    {
        initializeInstance();
        GameInstanceCameraInputActions = new GameInstanceCameraInputActions();
        activateInputActions();
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

    private void activateInputActions()
    {
        IsDragInputActionActive = true;
    }

    private void OnDestroy()
    {
        GameInstanceCameraInputActions.Camera.Drag.Disable();
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 originPoint;
    private Vector3 differenceVector;
    private bool isDragging;

    private Vector3 CurrentMousePosition => mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        InputActionsController.Instance.GameInstanceCameraInputActions.Camera.Drag.started += onDragStarted;
        InputActionsController.Instance.GameInstanceCameraInputActions.Camera.Drag.canceled += onDragCancelled;
    }

    private void OnDisable()
    {
        InputActionsController.Instance.GameInstanceCameraInputActions.Camera.Drag.started -= onDragStarted;
        InputActionsController.Instance.GameInstanceCameraInputActions.Camera.Drag.canceled -= onDragCancelled;
    }

    private void onDragStarted(InputAction.CallbackContext callbackContext)
    {
        originPoint = CurrentMousePosition;
        isDragging = true;
    }
    
    private void onDragCancelled(InputAction.CallbackContext callbackContext)
    {
        isDragging = false;
    }

    private void LateUpdate()
    {
        if (!isDragging) return;
        differenceVector = CurrentMousePosition - transform.position;
        transform.position = originPoint - differenceVector;
    }
}

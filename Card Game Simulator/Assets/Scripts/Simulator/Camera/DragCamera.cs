using UnityEngine;
using UnityEngine.InputSystem;

public class DragCamera : MonoBehaviour
{
    private Camera mainCamera;
    private Transform cameraTransform;
    private bool isDragging;
    private Vector3 originPoint;

    private Vector3 currentMousePosition => mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
    private SimulatorCameraControls simulatorCameraControls => CameraControlsManager.Instance.SimulatorCameraControls;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera?.transform;
    }

    private void OnEnable()
    {
        simulatorCameraControls.Camera.Drag.started += onDragStarted;
        simulatorCameraControls.Camera.Drag.canceled += onDragCancelled;
    }

    private void OnDisable()
    {
        if (CameraControlsManager.Instance != null && simulatorCameraControls != null)
        {
            simulatorCameraControls.Camera.Drag.started -= onDragStarted;
            simulatorCameraControls.Camera.Drag.canceled -= onDragCancelled;
        }
    }

    private void onDragStarted(InputAction.CallbackContext callbackContext)
    {
        originPoint = currentMousePosition;
        isDragging = true;
    }
    
    private void onDragCancelled(InputAction.CallbackContext callbackContext)
    {
        isDragging = false;
    }

    private void LateUpdate()
    {
        if (!isDragging) return;
        Vector3 differenceVector = currentMousePosition - cameraTransform.position;
        cameraTransform.position = originPoint - differenceVector;
    }
}

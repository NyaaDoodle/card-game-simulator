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

    public void OnDrag(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            originPoint = CurrentMousePosition;
        }
        isDragging = callbackContext.started || callbackContext.performed;
    }

    private void LateUpdate()
    {
        if (!isDragging) return;
        differenceVector = CurrentMousePosition - transform.position;
        transform.position = originPoint - differenceVector;
    }
}

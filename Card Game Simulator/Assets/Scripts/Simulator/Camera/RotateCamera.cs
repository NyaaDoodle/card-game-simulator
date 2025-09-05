using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float touchRotationSensitivity;
    [SerializeField] private float keyboardRotationSensitivity;

    private Transform mainCameraTransform;
    private Coroutine touchRotationDetectionCoroutine;
    private Coroutine checkKeyboardRotationCoroutine;
    
    private SimulatorCameraControls simulatorCameraControls => CameraControlsManager.Instance.SimulatorCameraControls;

    private void Awake()
    {
        mainCameraTransform = Camera.main?.transform;
    }

    private void OnEnable()
    {
        simulatorCameraControls.Camera.TwoFingerContact.started += onTwoFingerContactStart;
        simulatorCameraControls.Camera.TwoFingerContact.canceled += onTwoFingerContactEnd;
        checkKeyboardRotationCoroutine = StartCoroutine(checkKeyboardRotation());
    }

    private void OnDisable()
    {
        if (CameraControlsManager.Instance != null && simulatorCameraControls != null)
        {
            simulatorCameraControls.Camera.TwoFingerContact.started -= onTwoFingerContactStart;
            simulatorCameraControls.Camera.TwoFingerContact.canceled -= onTwoFingerContactEnd;
        }
        StopCoroutine(checkKeyboardRotationCoroutine);
    }

    private void onTwoFingerContactStart(InputAction.CallbackContext callbackContext)
    {
        touchRotationDetectionCoroutine = StartCoroutine(touchRotationDetection());
    }

    private void onTwoFingerContactEnd(InputAction.CallbackContext callbackContext)
    {
        StopCoroutine(touchRotationDetectionCoroutine);
    }

    private IEnumerator touchRotationDetection()
    {
        const float touchRotationThreshold = 0.1f;
        while (true)
        {
            Vector2 firstFingerPosition = simulatorCameraControls.Camera.FirstFingerPosition.ReadValue<Vector2>();
            Vector2 firstFingerDelta = simulatorCameraControls.Camera.FirstFingerDelta.ReadValue<Vector2>();
            Vector2 secondFingerPositon = simulatorCameraControls.Camera.SecondFingerPosition.ReadValue<Vector2>();
            Vector2 secondFingerDelta = simulatorCameraControls.Camera.SecondFingerDelta.ReadValue<Vector2>();
            Vector2 previousPositonVector =
                (secondFingerPositon - secondFingerDelta) - (firstFingerPosition - firstFingerDelta);
            Vector2 currentPositionVector = secondFingerPositon - firstFingerPosition;
            float angleData = Vector2.SignedAngle(previousPositonVector, currentPositionVector);
            if (Mathf.Abs(angleData) >= touchRotationThreshold)
            {
                transform.Rotate(0, 0, -angleData * touchRotationSensitivity);
            }
            yield return null;
        }
    }

    private IEnumerator checkKeyboardRotation()
    {
        const float keyboardRotationThreshold = 0.1f;
        while (true)
        {
            float rotationInput = simulatorCameraControls.Camera.KeyboardRotate.ReadValue<float>();
            if (Mathf.Abs(rotationInput) >= keyboardRotationThreshold)
            {
                float rotationAmount = rotationInput * keyboardRotationSensitivity * Time.deltaTime;
                mainCameraTransform.Rotate(0, 0, rotationAmount);
            }
            yield return null;
        }
    }
}

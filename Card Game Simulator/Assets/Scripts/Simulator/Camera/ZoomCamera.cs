using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float touchZoomSpeed;
    [SerializeField] private float mouseZoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    
    private Camera mainCamera;
    private Coroutine touchZoomDetectionCoroutine;
    private Coroutine checkMouseZoomCoroutine;
    
    private SimulatorCameraControls simulatorCameraControls => CameraControlsManager.Instance.SimulatorCameraControls;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void OnEnable()
    {
        simulatorCameraControls.Camera.TwoFingerContact.started += onTwoFingerContactStart;
        simulatorCameraControls.Camera.TwoFingerContact.canceled += onTwoFingerContactEnd;
        checkMouseZoomCoroutine = StartCoroutine(checkMouseZoom());
    }

    private void OnDisable()
    {
        if (CameraControlsManager.Instance != null && simulatorCameraControls != null)
        {
            simulatorCameraControls.Camera.TwoFingerContact.started -= onTwoFingerContactStart;
            simulatorCameraControls.Camera.TwoFingerContact.canceled -= onTwoFingerContactEnd;
        }
        StopCoroutine(checkMouseZoomCoroutine);
    }
    
    private void onTwoFingerContactStart(InputAction.CallbackContext callbackContext)
    {
        touchZoomDetectionCoroutine = StartCoroutine(touchZoomDetection());
    }

    private void onTwoFingerContactEnd(InputAction.CallbackContext callbackContext)
    {
        StopCoroutine(touchZoomDetectionCoroutine);
    }

    private IEnumerator touchZoomDetection()
    {
        while (true)
        {
            Vector2 firstFingerPosition = simulatorCameraControls.Camera.FirstFingerPosition.ReadValue<Vector2>();
            Vector2 firstFingerDelta = simulatorCameraControls.Camera.FirstFingerDelta.ReadValue<Vector2>();
            Vector2 secondFingerPositon = simulatorCameraControls.Camera.SecondFingerPosition.ReadValue<Vector2>();
            Vector2 secondFingerDelta = simulatorCameraControls.Camera.SecondFingerDelta.ReadValue<Vector2>();
            float previousDistance = Vector2.Distance(
                firstFingerPosition - firstFingerDelta,
                secondFingerPositon - secondFingerDelta);
            float currentDistance = Vector2.Distance(firstFingerPosition, secondFingerPositon);
            float distanceDelta = currentDistance - previousDistance;
            float zoomingFactor = distanceDelta * touchZoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - zoomingFactor, minZoom, maxZoom);
            yield return null;
        }
    }

    private IEnumerator checkMouseZoom()
    {
        const float mouseZoomThreshold = 0.01f;
        while (true)
        {
            float scrollValue = simulatorCameraControls.Camera.MouseScroll.ReadValue<Vector2>().y;
            if (Mathf.Abs(scrollValue) >= mouseZoomThreshold)
            {
                float zoomingFactor = -Mathf.Sign(scrollValue) * mouseZoomSpeed;
                mainCamera.orthographicSize = Mathf.Clamp(
                    mainCamera.orthographicSize + zoomingFactor,
                    minZoom,
                    maxZoom);
            }
            yield return null;
        }
    }
}
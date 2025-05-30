using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private ViewManager viewManager;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference primaryTouchAction;
    [SerializeField] private InputActionReference primaryTouchPositionAction;
    [SerializeField] private InputActionReference secondaryTouchPositionAction;
    [SerializeField] private InputActionReference scrollAction;

    [Header("Settings")]
    [SerializeField] private float dragThreshold = 10f; // Minimum distance to start drag

    private Camera gameCamera;
    private bool isDragging = false;
    private Vector2 lastTouchPosition;
    private Vector2 dragStartPosition;
    private float previousPinchDistance = 0f;

    // Events for different input types
    public System.Action<Vector2> OnTap;          // Single tap at position
    public System.Action<Vector2> OnDragStart;    // Drag started
    public System.Action<Vector2> OnDrag;         // Dragging
    public System.Action<Vector2> OnDragEnd;      // Drag ended
    public System.Action<float> OnZoom;           // Zoom delta
    public System.Action<Vector2> OnScroll;       // Scroll delta

    void Start()
    {
        gameCamera = Camera.main;
        setupInputActions();
    }

    void OnEnable()
    {
        enableInputActions();
    }

    void OnDisable()
    {
        disableInputActions();
    }

    private void setupInputActions()
    {
        if (primaryTouchAction != null)
        {
            primaryTouchAction.action.performed += onPrimaryTouchPerformed;
            primaryTouchAction.action.canceled += onPrimaryTouchCanceled;
        }

        if (scrollAction != null)
        {
            scrollAction.action.performed += onScrollPerformed;
        }
    }

    private void enableInputActions()
    {
        primaryTouchAction?.action.Enable();
        primaryTouchPositionAction?.action.Enable();
        secondaryTouchPositionAction?.action.Enable();
        scrollAction?.action.Enable();
    }

    private void disableInputActions()
    {
        primaryTouchAction?.action.Disable();
        primaryTouchPositionAction?.action.Disable();
        secondaryTouchPositionAction?.action.Disable();
        scrollAction?.action.Disable();
    }

    private void onPrimaryTouchPerformed(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = getTouchPosition();

        if (!isDragging)
        {
            dragStartPosition = touchPosition;
            lastTouchPosition = touchPosition;
        }
    }

    private void onPrimaryTouchCanceled(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = getTouchPosition();

        if (isDragging)
        {
            // End of drag
            OnDragEnd?.Invoke(touchPosition);
            isDragging = false;
        }
        else
        {
            // Tapping action
            OnTap?.Invoke(touchPosition);
        }
    }

    private void onScrollPerformed(InputAction.CallbackContext context)
    {
        Vector2 scrollDelta = context.ReadValue<Vector2>();

        if (viewManager != null && viewManager.IsInTableView)
        {
            // In table view, vertical scroll is zoom
            if (Mathf.Abs(scrollDelta.y) > 0.1f)
            {
                OnZoom?.Invoke(scrollDelta.y);
            }
        }
        else
        {
            // In other views, treat as normal scroll
            OnScroll?.Invoke(scrollDelta);
        }
    }

    void Update()
    {
        handleDragDetection();
        handlePinchZoom();
    }

    private void handleDragDetection()
    {
        if (primaryTouchAction == null || !primaryTouchAction.action.IsPressed())
            return;

        Vector2 currentPosition = getTouchPosition();

        if (!isDragging)
        {
            // Check if moved far enough to start dragging
            float dragDistance = Vector2.Distance(currentPosition, dragStartPosition);
            if (dragDistance > dragThreshold)
            {
                isDragging = true;
                OnDragStart?.Invoke(dragStartPosition);
            }
        }
        else
        {
            // Continue dragging
            OnDrag?.Invoke(currentPosition - lastTouchPosition);
        }

        lastTouchPosition = currentPosition;
    }

    private void handlePinchZoom()
    {
        if (primaryTouchPositionAction != null && secondaryTouchPositionAction != null)
        {
            bool touch1Active = primaryTouchPositionAction.action.IsPressed();
            bool touch2Active = secondaryTouchPositionAction.action.IsPressed();

            if (touch1Active && touch2Active)
            {
                // When pinch occurs
                Vector2 touch1Pos = primaryTouchPositionAction.action.ReadValue<Vector2>();
                Vector2 touch2Pos = secondaryTouchPositionAction.action.ReadValue<Vector2>();

                float currentPinchDistance = Vector2.Distance(touch1Pos, touch2Pos);

                if (previousPinchDistance > 0f)
                {
                    float deltaDistance = currentPinchDistance - previousPinchDistance;
                    OnZoom?.Invoke(deltaDistance * 0.01f); // Scale factor
                }
                previousPinchDistance = currentPinchDistance;
            }
            else
            {
                // Pinch stopped
                previousPinchDistance = 0f;
            }
        }
    }

    private Vector2 getTouchPosition()
    {
        if (primaryTouchPositionAction != null)
        {
            return primaryTouchPositionAction.action.ReadValue<Vector2>();
        }
        return Input.mousePosition;
    }

    void OnDestroy()
    {
        if (primaryTouchAction != null)
        {
            primaryTouchAction.action.performed -= onPrimaryTouchPerformed;
            primaryTouchAction.action.canceled -= onPrimaryTouchCanceled;
        }

        if (scrollAction != null)
        {
            scrollAction.action.performed -= onScrollPerformed;
        }
    }
}

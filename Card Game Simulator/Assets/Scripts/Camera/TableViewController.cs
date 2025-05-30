using UnityEngine;

public class TableViewController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    void Start()
    {
        inputController.OnDrag += handleCameraDrag;
        inputController.OnZoom += handleCameraZoom;
    }

    private void handleCameraDrag(Vector2 distanceDelta)
    {

    }

    private void handleCameraZoom(float scrollDelta)
    {

    }
}

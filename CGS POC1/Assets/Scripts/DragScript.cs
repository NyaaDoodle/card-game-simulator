using UnityEngine;

public class DragScript : MonoBehaviour
{
    private bool isDragging = false;

    private void Update()
    {
        if (!isDragging) return;
        transform.position = getFixedPosition();
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        isDragging = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        isDragging = false;
    }

    private Vector3 getFixedPosition()
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);
    }
}

using UnityEngine;

public class HandViewController : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    void Start()
    {
        inputController.OnScroll += handleScroll;
    }

    private void handleScroll(Vector2 scrollDelta)
    {

    }
}

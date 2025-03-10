using UnityEngine;
using UnityEngine.UI;

public class SizeTestScript : MonoBehaviour
{
    void Start()
    {
        this.gameObject.AddComponent<Image>();
        Image imageComponent = this.gameObject.GetComponent<Image>();
        imageComponent.sprite = Resources.Load<Sprite>("AD");
        RectTransform rectTransformComponent = this.gameObject.GetComponent<RectTransform>();
        rectTransformComponent.sizeDelta = new Vector2(100, 100);
    }
}

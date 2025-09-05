using System.Collections;
using TMPro;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    private Coroutine showThenHideCoroutine;

    public void Setup(string text, float durationInSeconds, Color textColor)
    {
        textComponent.text = text;
        textComponent.color = textColor;
        showThenHideCoroutine = StartCoroutine(showThenHide(durationInSeconds));
    }

    public void Close()
    {
        if (showThenHideCoroutine != null)
        {
            StopCoroutine(showThenHideCoroutine);
            Destroy(gameObject);
        }
    }

    private IEnumerator showThenHide(float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        Destroy(gameObject);
    }
}

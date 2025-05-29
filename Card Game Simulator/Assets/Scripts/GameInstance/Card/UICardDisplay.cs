using Mirror.SimpleWeb;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardState))]
public class UICardDisplay : MonoBehaviour
{
    private Image cardImage;
    private CardState cardState;

    void Awake()
    {
        cardImage = GetComponent<Image>();
        cardState = GetComponent<CardState>();
    }

    void Start()
    {
        cardState.Defined += CardState_OnDefined;
        updateDisplay();
    }

    private void CardState_OnDefined(CardState _)
    {
        updateDisplay();
    }

    private void updateDisplay()
    {
        if (cardImage != null && cardState.IsDefined)
        {
            cardImage.sprite = cardState.FrontSideSprite;
        }
    }
}

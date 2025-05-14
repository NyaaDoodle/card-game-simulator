using System;
using UnityEngine;

public class CardState : MonoBehaviour
{
    [SerializeField] private GameObject frontSideGameObject;
    [SerializeField] private GameObject backSideGameObject;
    public CardData CardData { get; private set; } = null;
    public bool IsFaceUp { get; private set; } = false;
    public bool IsShown { get; private set; } = false;
    public bool IsDefined => CardData != null;
    public float? Width => CardData?.Width;
    public float? Height => CardData?.Height;
    public GameObject FrontSideGameObject => frontSideGameObject;
    public GameObject BackSideGameObject => backSideGameObject;
    public Sprite BackSideSprite => CardData?.BackSideSprite;
    public Sprite FrontSideSprite => CardData?.FrontSideSprite;

    public event Action<CardState> Flipped;
    public event Action<CardState> Hidden;
    public event Action<CardState> Shown;
    public event Action<CardState> Defined;

    public void SetCardData(CardData cardData)
    {
        if (!IsDefined)
        {
            CardData = cardData;
            setTransformAccordingToCardData();
            OnDefined();
        }
        else
        {
            Debug.Log($"SetCardData(): Card with id {CardData.Id} has already been set");
        }
    }

    public void Flip()
    {
        IsFaceUp = !IsFaceUp;
        OnFlipped();
    }

    public void HideCard()
    {
        IsShown = false;
        OnHidden();
    }

    public void ShowCard()
    {
        IsShown = true;
        OnShown();
    }

    protected virtual void OnFlipped()
    {
        Flipped?.Invoke(this);
    }

    protected virtual void OnHidden()
    {
        Hidden?.Invoke(this);
    }

    protected virtual void OnShown()
    {
        Shown?.Invoke(this);
    }

    protected virtual void OnDefined()
    {
        Defined?.Invoke(this);
    }

    private void setTransformAccordingToCardData()
    {
        //this.transform.localPosition = CardData.LocationOnTable;
    }
}

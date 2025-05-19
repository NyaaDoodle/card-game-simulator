using System;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // GameObjects attached to Card prefab
    [field: SerializeField] public GameObject FrontSideGameObject { get; private set; }
    [field: SerializeField] public GameObject BackSideGameObject { get; private set; }

    // CardData related properties
    public CardData CardData { get; private set; } = null;
    public bool IsDefined => CardData != null;
    public int? Id => CardData.Id;
    public string Name => CardData.Name;
    public string Description => CardData.Description;
    public float? Width => CardData?.Width;
    public float? Height => CardData?.Height;

    public Sprite BackSideSprite { get; private set; } = null;
    public Sprite FrontSideSprite { get; private set; } = null;

    public bool IsFaceUp { get; private set; } = false;
    public bool IsShown { get; private set; } = false;
    
    public event Action<CardState> Flipped;
    public event Action<CardState> Hidden;
    public event Action<CardState> Shown;
    public event Action<CardState> Defined;

    public void SetCardData(CardData cardData)
    {
        if (!IsDefined)
        {
            CardData = cardData;
            loadCardSprites();
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

    private void loadCardSprites()
    {
        // TODO allow loading outside of Resources folder
        BackSideSprite = Resources.Load<Sprite>(CardData.BackSideSpritePath);
        if (BackSideSprite == null)
        {
            Debug.LogError($"CardState: Failed to load sprite at {CardData.BackSideSpritePath}");
        }

        FrontSideSprite = Resources.Load<Sprite>(CardData.FrontSideSpritePath);
        if (FrontSideSprite == null)
        {
            Debug.LogError($"CardState: Failed to load sprite at {CardData.FrontSideSpritePath}");
        }
    }
}

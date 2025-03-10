using System;
using UnityEngine;

public class CardState : MonoBehaviour
{
    public CardData CardData { get; set; }
    public bool IsFaceUp { get; set; }
    public float Width => CardData.Width;
    public float Height => CardData.Height;
    public Sprite FrontSideSprite => CardData?.FrontSideSprite;
    public Sprite BackSideSprite => CardData?.BackSideSprite;
    public bool IsDefined => CardData != null;

    public event Action<CardState> Flipped; 
    
    void Start()
    {
        CardData = null;
        IsFaceUp = false;
    }

    public void Flip()
    {
        OnFlipped();
    }

    protected virtual void OnFlipped()
    {
        Flipped?.Invoke(this);
    }
}

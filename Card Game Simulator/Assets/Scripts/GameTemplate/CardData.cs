using UnityEngine;

public class CardData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Sprite BackSideSprite { get; set; }
    public Sprite FrontSideSprite { get; set; }
    public Vector2 LocationOnTable { get; set; }
}
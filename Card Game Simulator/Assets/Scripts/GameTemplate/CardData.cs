﻿using UnityEngine;

public class CardData
{
    private const float defaultWidth = 2f;
    private const float defaultHeight = 3f;

    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public float Width { get; set; } = defaultWidth;
    public float Height { get; set; } = defaultHeight;
    public string BackSideSpritePath { get; set; } = "";
    public string FrontSideSpritePath { get; set; } = "";
}
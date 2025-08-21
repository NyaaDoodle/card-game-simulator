using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct GameTemplateDetails : IEquatable<GameTemplateDetails>
{
    public string TemplateName { get; }
    public string CreatorName { get; }
    public string Description { get; }
    public string TemplateImagePath { get; }

    [JsonConstructor]
    public GameTemplateDetails(string templateName, string creatorName, string description, string templateImagePath)
    {
        TemplateName = templateName;
        CreatorName = creatorName;
        Description = description;
        TemplateImagePath = templateImagePath;
    }

    public bool Equals(GameTemplateDetails other)
    {
        return other.TemplateName == this.TemplateName && other.CreatorName == this.CreatorName
                                                       && other.Description == this.Description
                                                       && other.TemplateImagePath == this.TemplateImagePath;
    }

    public override bool Equals(object obj)
    {
        return obj is GameTemplateDetails gameTemplateDetails && this.Equals(gameTemplateDetails);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TemplateName, CreatorName, Description, TemplateImagePath);
    }

    public static bool operator==(GameTemplateDetails gameTemplateDetails1, GameTemplateDetails gameTemplateDetails2)
    {
        return gameTemplateDetails1.Equals(gameTemplateDetails2);
    }

    public static bool operator!=(GameTemplateDetails gameTemplateDetails1, GameTemplateDetails gameTemplateDetails2)
    {
        return !gameTemplateDetails1.Equals(gameTemplateDetails2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("GameTemplateDetails");
        stringBuilder.AppendLine($"TemplateName: {TemplateName}");
        stringBuilder.AppendLine($"CreatorName: {CreatorName}");
        stringBuilder.AppendLine($"Description: {Description}");
        stringBuilder.AppendLine($"TemplateImagePath: {TemplateImagePath}");
        return stringBuilder.ToString();
    }
}
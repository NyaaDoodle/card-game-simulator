using System;
using System.Collections.Generic;
using System.Text;

public readonly struct ImageAssetReference : IEquatable<ImageAssetReference>
{
    public string ImageId { get; }
    public string Filename { get; }
    // FilePath is local from the persistent data directory.
    public string FilePath { get; }

    public ImageAssetReference(string imageId, string filename, string filePath)
    {
        ImageId = imageId;
        Filename = filename;
        FilePath = filePath;
    }

    // TODO remove
    public ImageAssetReference(string path)
    {
        ImageId = System.Guid.NewGuid().ToString();
        Filename = path;
        FilePath = path;
    }

    public bool Equals(ImageAssetReference other)
    {
        return other.ImageId == this.ImageId && other.Filename == this.Filename && other.FilePath == this.FilePath;
    }

    public override bool Equals(object obj)
    {
        return obj is ImageAssetReference other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ImageId, Filename, FilePath);
    }

    public static bool operator==(ImageAssetReference imageAssetReference1, ImageAssetReference imageAssetReference2)
    {
        return imageAssetReference1.Equals(imageAssetReference2);
    }
    
    public static bool operator!=(ImageAssetReference imageAssetReference1, ImageAssetReference imageAssetReference2)
    {
        return !imageAssetReference1.Equals(imageAssetReference2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("ImageAssetReference");
        stringBuilder.AppendLine($"ImageId: {ImageId}");
        stringBuilder.AppendLine($"Filename: {Filename}");
        stringBuilder.AppendLine($"FilePath: {FilePath}");
        return stringBuilder.ToString();
    }
}

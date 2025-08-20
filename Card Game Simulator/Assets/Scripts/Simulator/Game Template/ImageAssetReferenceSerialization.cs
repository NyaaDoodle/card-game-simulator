using Mirror;

public static class ImageAssetReferenceSerialization
{
    public static void WriteImageAssetReference(this NetworkWriter writer, ImageAssetReference imageAssetReference)
    {
        writer.WriteString(imageAssetReference.ImageId);
        writer.WriteString(imageAssetReference.Filename);
        writer.WriteString(imageAssetReference.FilePath);
    }

    public static ImageAssetReference ReadImageAssetReference(this NetworkReader reader)
    {
        string imageId = reader.ReadString();
        string filename = reader.ReadString();
        string filePath = reader.ReadString();
        return new ImageAssetReference(imageId, filename, filePath);
    }
}

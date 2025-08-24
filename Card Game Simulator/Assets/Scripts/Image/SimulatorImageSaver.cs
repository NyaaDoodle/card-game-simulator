using System;
using System.IO;
using UnityEngine;

public static class SimulatorImageSaver
{
    public static void SaveThumbnail(
        Texture2D texture,
        string gameTemplateId,
        Action<string> onSaveSuccess,
        Action<Exception> onSaveFailed)
    {
        checkGameTemplateThumbnailsDirectoryExists(gameTemplateId);
        try
        {
            byte[] textureJpgBytes = texture.EncodeToJPG();
            string thumbnailId = Guid.NewGuid().ToString();
            string thumbnailFilename = $"{thumbnailId}.jpg";
            string thumbnailPath = Path.Combine(getGameTemplateThumbnailsDirectory(gameTemplateId), thumbnailFilename);
            File.WriteAllBytes(thumbnailPath, textureJpgBytes);
            Debug.Log($"Written thumbnail image to {thumbnailPath}");
            onSaveSuccess(getPersistentDataLocalThumbnailImagePath(thumbnailFilename, gameTemplateId));
        }
        catch (Exception e)
        {
            onSaveFailed(e);
        }
    }

    public static void SaveImage(
        Texture2D texture,
        string gameTemplateId,
        Action<string> onSaveSuccess,
        Action<Exception> onSaveFailed)
    {
        checkGameTemplateImagesDirectoryExists(gameTemplateId);
        try
        {
            byte[] textureJpgBytes = texture.EncodeToJPG();
            string imageId = Guid.NewGuid().ToString();
            string imageFilename = $"{imageId}.jpg";
            string imagePath = Path.Combine(getGameTemplateImagesDirectory(gameTemplateId), imageFilename);
            File.WriteAllBytes(imagePath, textureJpgBytes);
            Debug.Log($"Written image to {imagePath}");
            onSaveSuccess(getPersistentDataLocalImagePath(imageFilename, gameTemplateId));
        }
        catch (Exception e)
        {
            onSaveFailed(e);
        }
    }

    private static void checkGameTemplateImagesDirectoryExists(string gameTemplateId)
    {
        string gameTemplateImagesDirectoryPath = getGameTemplateImagesDirectory(gameTemplateId);
        if (!Directory.Exists(gameTemplateImagesDirectoryPath))
        {
            Directory.CreateDirectory(gameTemplateImagesDirectoryPath);
        }
    }

    private static void checkGameTemplateThumbnailsDirectoryExists(string gameTemplateId)
    {
        string gameTemplateThumbnailsDirectoryPath = getGameTemplateThumbnailsDirectory(gameTemplateId);
        if (!Directory.Exists(gameTemplateThumbnailsDirectoryPath))
        {
            Directory.CreateDirectory(gameTemplateThumbnailsDirectoryPath);
        }
    }

    private static string getGameTemplateImagesDirectory(string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.Instance.ImagesDirectoryPath, gameTemplateId);
    }

    private static string getGameTemplateThumbnailsDirectory(string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.Instance.ThumbnailsDirectoryPath, gameTemplateId);
    }

    private static string getPersistentDataLocalImagePath(string imageFilename, string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.ImagesDirectoryName, gameTemplateId, imageFilename);
    }

    private static string getPersistentDataLocalThumbnailImagePath(string thumbnailFilename, string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.ThumbnailsDirectoryName, gameTemplateId, thumbnailFilename);
    }
}

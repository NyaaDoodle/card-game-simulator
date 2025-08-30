using System;
using System.IO;
using UnityEngine;

public static class SimulatorImageSaver
{
    private const int imageExportQuality = 85;

    public static void SaveImage(
        Texture2D texture,
        string gameTemplateId,
        Action<string> onSaveSuccess,
        Action<Exception> onSaveFailed)
    {
        string imageId = Guid.NewGuid().ToString();
        string imageFilename = $"{imageId}.jpg";
        SaveImageWithFilename(texture, imageFilename, gameTemplateId, onSaveSuccess, onSaveFailed);
    }
    
    public static void SaveImageWithFilename(
        Texture2D texture,
        string filename,
        string gameTemplateId,
        Action<string> onSaveSuccess,
        Action<Exception> onSaveFailed)
    {
        checkGameTemplateImagesDirectoryExists(gameTemplateId);
        try
        {
            string imagePath = Path.Combine(getGameTemplateImagesDirectory(gameTemplateId), filename);
            byte[] textureJpgBytes = texture.EncodeToJPG(imageExportQuality);
            File.WriteAllBytes(imagePath, textureJpgBytes);
            Debug.Log($"Written image to {imagePath}");
            onSaveSuccess?.Invoke(getPersistentDataLocalImagePath(filename, gameTemplateId));
        }
        catch (Exception e)
        {
            onSaveFailed?.Invoke(e);
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

    private static string getGameTemplateImagesDirectory(string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.Instance.ImagesDirectoryPath, gameTemplateId);
    }

    private static string getPersistentDataLocalImagePath(string imageFilename, string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.ImagesDirectoryName, gameTemplateId, imageFilename);
    }
}

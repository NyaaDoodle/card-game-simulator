using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class SimulatorImageLoader
{
    public static void LoadImageLocalPath(string localPath, Action<Sprite> onSuccessAction, Action<Exception> onFailureAction)
    {
        LoadImageAbsolutePath(GetFullImagePath(localPath), onSuccessAction, onFailureAction);
    }

    public static void LoadImageAbsolutePath(
        string path,
        Action<Sprite> onSuccessAction,
        Action<Exception> onFailureAction)
    {
        Debug.Log($"Attempting to load image from: {path}");
        Sprite sprite = loadImage(path, onFailureAction);
        onSuccessAction?.Invoke(sprite);
    }

    public static void LoadImageLocalPath(string localPath, Image imageComponent, Sprite fallbackSprite)
    {
        LoadImageLocalPath(localPath,
            (sprite) => imageComponent.sprite = sprite,
            (_) =>
                {
                    imageComponent.sprite = fallbackSprite;
                });
    }

    public static void LoadImageAbsolutePath(string path, Image imageComponent, Sprite fallbackSprite)
    {
        LoadImageAbsolutePath(path,
            (sprite) => imageComponent.sprite = sprite,
            (_) =>
                {
                    imageComponent.sprite = fallbackSprite;
                });
    }

    public static string GetFullImagePath(string localPath)
    {
        // localPath needs to be local from the point of the persistent data directory
        return Path.Combine(DataDirectoryManager.Instance.DataDirectoryPath, localPath);
    }
    
    private static Sprite loadImage(string filePath, Action<Exception> onFailureAction)
    {
        if (!File.Exists(filePath))
        {
            onFailureAction?.Invoke(new Exception("File does not exist"));
            return null;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(1, 1);
        
        if (texture.LoadImage(fileData))
        {
            return Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f));
        }
        
        UnityEngine.Object.DestroyImmediate(texture);
        onFailureAction?.Invoke(new Exception("Failed to texture.LoadImage(fileData)"));
        return null;
    }

}

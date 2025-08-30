using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class SimulatorImageLoader
{
    public static void LoadTextureLocalPath(
        string localPath,
        Action<Texture2D> onSuccessAction,
        Action<Exception> onFailureAction)
    {
        LoadTextureAbsolutePath(GetFullFilePath(localPath), onSuccessAction, onFailureAction);
    }

    public static void LoadTextureAbsolutePath(
        string path,
        Action<Texture2D> onSuccessAction,
        Action<Exception> onFailureAction)
    {
        Debug.Log($"Attempting to load texture from: {path}");
        Texture2D texture = loadTexture(path, onFailureAction);
        if (texture != null)
        {
            onSuccessAction?.Invoke(texture);
        }
    }
    
    public static void LoadSpriteLocalPath(string localPath, Action<Sprite> onSuccessAction, Action<Exception> onFailureAction)
    {
        LoadSpriteAbsolutePath(GetFullFilePath(localPath), onSuccessAction, onFailureAction);
    }

    public static void LoadSpriteAbsolutePath(
        string path,
        Action<Sprite> onSuccessAction,
        Action<Exception> onFailureAction)
    {
        Sprite sprite = loadSprite(path, onFailureAction);
        if (sprite != null)
        {
            onSuccessAction?.Invoke(sprite);
        }
    }

    public static void LoadSpriteLocalPath(string localPath, Image imageComponent, Sprite fallbackSprite)
    {
        LoadSpriteLocalPath(localPath,
            (sprite) => imageComponent.sprite = sprite,
            (_) =>
                {
                    imageComponent.sprite = fallbackSprite;
                });
    }

    public static void LoadSpriteAbsolutePath(string path, Image imageComponent, Sprite fallbackSprite)
    {
        LoadSpriteAbsolutePath(path,
            (sprite) => imageComponent.sprite = sprite,
            (_) =>
                {
                    imageComponent.sprite = fallbackSprite;
                });
    }

    public static string GetFullFilePath(string localPath)
    {
        // localPath needs to be local from the point of the persistent data directory
        return Path.Combine(DataDirectoryManager.Instance.DataDirectoryPath, localPath);
    }

    private static Texture2D loadTexture(string filePath, Action<Exception> onFailureAction)
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
            return texture;
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(texture);
            onFailureAction?.Invoke(new Exception("Failed to texture.LoadImage(fileData)"));
            return null;
        }
        
    }
    
    private static Sprite loadSprite(string filePath, Action<Exception> onFailureAction)
    {
        Texture2D texture = loadTexture(filePath, onFailureAction);
        
        if (texture != null)
        {
            return Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f));
        }
        else
        {
            return null;
        }
    }
}

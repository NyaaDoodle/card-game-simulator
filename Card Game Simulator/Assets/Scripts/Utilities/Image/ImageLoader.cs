using System.IO;
using UnityEngine;

public static class ImageLoader
{
    public static Sprite LoadImageAsset(string pathToImageFile)
    {
        // Used for loading images that are not build assets (Resources)
        if (!File.Exists(pathToImageFile))
        {
            Debug.LogWarning($"File at {pathToImageFile} does not exist");
            return null;
        }

        Texture2D loadedTexture = loadTexture(pathToImageFile);
        if (loadedTexture != null)
        {
            Rect spriteRect = new Rect(0, 0, loadedTexture.width, loadedTexture.height);
            Vector2 pivotVector = Vector2.one * 0.5f;
            return Sprite.Create(loadedTexture, spriteRect, pivotVector);
        }
        else
        {
            Debug.LogWarning("LoadedTexture after loadTexture() is null");
            return null;
        }
    }

    public static Sprite LoadFallbackImage(string pathToFallbackImage)
    {
        // Used to load fallback images, must be image assets inside the Resources directory
        return Resources.Load<Sprite>(pathToFallbackImage);
    }

    private static Texture2D loadTexture(string pathToTextureFile)
    {
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        byte[] fileData = File.ReadAllBytes(pathToTextureFile);
        bool isLoaded = texture.LoadImage(fileData, false);
        Debug.Log($"{pathToTextureFile} {isLoaded}");
        return texture;
    }
}

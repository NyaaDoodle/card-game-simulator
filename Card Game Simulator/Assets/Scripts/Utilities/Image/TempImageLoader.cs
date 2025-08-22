using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TempImageLoader : MonoBehaviour
{
    public static TempImageLoader Instance { get; private set; }

    private void Awake()
    {
        initializeInstance();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void LoadImageAsset(string pathToImageFile, Image imageComponent)
    {
        // Used for loading images that are not build assets (Resources)
        if (!File.Exists(pathToImageFile))
        {
            Debug.LogWarning($"File at {pathToImageFile} does not exist");
            return;
        }
        
        Texture2D loadedTexture = loadTexture(pathToImageFile);
        if (loadedTexture != null)
        {
            Rect spriteRect = new Rect(0, 0, loadedTexture.width, loadedTexture.height);
            Vector2 pivotVector = Vector2.one * 0.5f;
            Sprite sprite = Sprite.Create(loadedTexture, spriteRect, pivotVector);
            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("LoadedTexture after loadTexture() is null");
        }
    }

    public Sprite LoadFallbackImage(string pathToFallbackImage)
    {
        // Used to load fallback images, must be image assets inside the Resources directory
        return Resources.Load<Sprite>(pathToFallbackImage);
    }

    private Texture2D loadTexture(string pathToTextureFile)
    {
        Texture2D texture = new Texture2D(2, 2);
        byte[] fileData = File.ReadAllBytes(pathToTextureFile);
        bool isLoaded = ImageConversion.LoadImage(texture, fileData);
        Debug.Log($"{pathToTextureFile} {isLoaded}");
        return texture;
    }
}

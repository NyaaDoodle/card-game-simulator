using System;
using System.IO;
using Extensions.Unity.ImageLoader;
using UnityEngine;
using UnityEngine.UI;

public static class SimulatorImageLoader
{
    public static void LoadImage(string localPath, Action<Sprite> onSuccessAction, Action<Exception> onFailureAction)
    {
        string fullPath = GetFullImagePath(localPath);
        ImageLoader.LoadSprite(fullPath)
            .Consume(onSuccessAction)
            .Failed(onFailureAction)
            .Forget();
    }

    public static void LoadImage(string localPath, Image imageComponent, Sprite fallbackSprite)
    {
        LoadImage(localPath,
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
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ContentDownloader
{
    public static void SetServerToCloudBackendServer()
    {
        ContentServerAPIManager.Instance.SetServerToCloudBackendServer();
    }

    public static void SetServerURL(string serverIP, int serverPort)
    {
        ContentServerAPIManager.Instance.SetServerURL(serverIP, serverPort);
    }
    
    public static void GetCompleteAvailableGameTemplates(Action onSuccess, Action<string> onError)
    {
        Debug.Log("Attempting to download game templates");
        ContentServerAPIManager.Instance.GetAvailableGameTemplates(
            (list) => onGetAvailableGameTemplatesList(list, onSuccess, onError),
            (error) => onError?.Invoke(error));
    }

    private static void onGetAvailableGameTemplatesList(List<string> gameTemplateIdList, Action onSuccess, Action<string> onError)
    {
        if (gameTemplateIdList.Count == 0)
        {
            onSuccess?.Invoke();
            return;
        }
        DownloadSession downloadSession = new DownloadSession() { OnSuccess = onSuccess, OnError = onError };
        List<string> requiredGameTemplatesToDownload = getRequiredGameTemplatesToDownloadList(gameTemplateIdList);
        Debug.Log($"{requiredGameTemplatesToDownload.Count} game templates are required to be downloaded");
        downloadSession.TotalOperations += requiredGameTemplatesToDownload.Count;
        foreach (string templateId in requiredGameTemplatesToDownload)
        {
            getGameTemplateData(templateId, downloadSession);
        }
    }

    private static List<string> getRequiredGameTemplatesToDownloadList(List<string> availableGameTemplateIdList)
    {
        List<string> requiredGameTemplatesToDownload = new List<string>();
        foreach (string templateId in availableGameTemplateIdList)
        {
            if (!GameTemplateLoader.isGameTemplateDataFileStored(templateId))
            {
                requiredGameTemplatesToDownload.Add(templateId);
            }
        }
        return requiredGameTemplatesToDownload;
    }
    
    private static void getGameTemplateData(string templateId, DownloadSession downloadSession)
    {
        Debug.Log($"Attempting to download game template {templateId}");
        ContentServerAPIManager.Instance.GetGameTemplateData(
            templateId,
            (gameTemplate) => onGetGameTemplate(gameTemplate, downloadSession),
            downloadSession.ReportError);
    }

    private static void onGetGameTemplate(GameTemplate gameTemplate, DownloadSession downloadSession)
    {
        try
        {
            GameTemplateSaver.SaveGameTemplate(gameTemplate);
        }
        catch (Exception e)
        {
            downloadSession.ReportError($"Failed to save game template {gameTemplate.Id}: {e.Message}");
            return;
        }
        List<string> requiredImagesToDownload = getRequiredImagesToDownloadList(gameTemplate);
        downloadSession.TotalOperations += requiredImagesToDownload.Count;
        foreach (string imageFilename in requiredImagesToDownload)
        {
            getImage(imageFilename, gameTemplate, downloadSession);
        }
        downloadSession.CompleteOneOperation();
    }

    private static List<string> getRequiredImagesToDownloadList(GameTemplate gameTemplate)
    {
        List<string> requiredImagesToDownload = new List<string>();
        string thumbnailFilename = getTemplateThumbnailFilename(gameTemplate.GameTemplateDetails);
        string imageDirectoryPath = DataDirectoryManager.Instance.ImagesDirectoryPath;
        if (!string.IsNullOrEmpty(thumbnailFilename))
        {
            if (!File.Exists(Path.Combine(imageDirectoryPath, thumbnailFilename)))
            {
                requiredImagesToDownload.Add(thumbnailFilename);
            }
        }
        string tableSurfaceImageFilename = getTableSurfaceImageFilename(gameTemplate.TableData);
        if (!string.IsNullOrEmpty(tableSurfaceImageFilename))
        {
            if (!File.Exists(Path.Combine(imageDirectoryPath, tableSurfaceImageFilename)))
            {
                requiredImagesToDownload.Add(tableSurfaceImageFilename);
            }
        }
        foreach (CardData cardData in gameTemplate.CardPool)
        {
            string frontSideImageFilename = getFrontSideImageFilename(cardData);
            if (!string.IsNullOrEmpty(frontSideImageFilename))
            {
                if (!File.Exists(Path.Combine(imageDirectoryPath, frontSideImageFilename)))
                {
                    requiredImagesToDownload.Add(frontSideImageFilename);
                }
            }
            string backSideImageFilename = getBackSideImageFilename(cardData);
            if (!string.IsNullOrEmpty(backSideImageFilename))
            {
                if (!File.Exists(Path.Combine(imageDirectoryPath, backSideImageFilename)))
                {
                    requiredImagesToDownload.Add(backSideImageFilename);
                }
            }
        }
        return requiredImagesToDownload;
    }

    private static void getImage(string imageFilename, GameTemplate gameTemplate, DownloadSession downloadSession)
    {
        Debug.Log($"Attempting to download image {imageFilename} of game template {gameTemplate.Id}");
        ContentServerAPIManager.Instance.GetImageTexture(
            imageFilename,
            gameTemplate.Id,
            (texture) => onGetImage(texture, imageFilename, gameTemplate, downloadSession),
            downloadSession.ReportError);
    }

    private static void onGetImage(Texture2D texture, string imageFilename, GameTemplate gameTemplate, DownloadSession downloadSession)
    {
        SimulatorImageSaver.SaveImageWithFilename(
            texture,
            imageFilename,
            gameTemplate.Id,
            (_) => downloadSession.CompleteOneOperation(),
            (e) => downloadSession.ReportError(e.Message));
    }

    private static string getTemplateThumbnailFilename(GameTemplateDetails gameTemplateDetails)
    {
        string thumbnailPath = gameTemplateDetails.TemplateImagePath;
        if (!string.IsNullOrEmpty(thumbnailPath))
        {
            return Path.GetFileName(thumbnailPath);
        }
        else
        {
            return null;
        }
    }

    private static string getTableSurfaceImageFilename(TableData tableData)
    {
        string tableSurfaceImagePath = tableData.SurfaceImagePath;
        if (!string.IsNullOrEmpty(tableSurfaceImagePath))
        {
            return Path.GetFileName(tableSurfaceImagePath);
        }
        else
        {
            return null;
        }
    }

    private static string getFrontSideImageFilename(CardData cardData)
    {
        string frontSideImagePath = cardData.FrontSideImagePath;
        if (!string.IsNullOrEmpty(frontSideImagePath))
        {
            return Path.GetFileName(frontSideImagePath);
        }
        else
        {
            return null;
        }
    }

    private static string getBackSideImageFilename(CardData cardData)
    {
        string backSideImagePath = cardData.BackSideImagePath;
        if (!string.IsNullOrEmpty(backSideImagePath))
        {
            return Path.GetFileName(backSideImagePath);
        }
        else
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ContentDownloader
{
    public static void GetCompleteAvailableGameTemplates(DownloadSession downloadSession)
    {
        Debug.Log($"Attempting to download game templates from {downloadSession.ServerIP} at port {downloadSession.ServerPort}");
        ContentServerAPIManager.Instance.GetAvailableGameTemplates(
            downloadSession.ServerIP,
            downloadSession.ServerPort,
            (list) => onGetAvailableGameTemplatesList(list, downloadSession),
            (error) => downloadSession.OnError?.Invoke(error, null));
    }

    private static void onGetAvailableGameTemplatesList(List<string> gameTemplateIdList, DownloadSession downloadSession)
    {
        if (gameTemplateIdList.Count == 0)
        {
            downloadSession.OnSuccess?.Invoke();
            return;
        }
        List<string> requiredGameTemplatesToDownload = getRequiredGameTemplatesToDownloadList(gameTemplateIdList);
        Debug.Log($"{requiredGameTemplatesToDownload.Count} game templates are required to be downloaded");
        foreach (string templateId in requiredGameTemplatesToDownload)
        {
            GetGameTemplate(templateId, downloadSession);
        }
    }

    private static List<string> getRequiredGameTemplatesToDownloadList(List<string> availableGameTemplateIdList)
    {
        List<string> requiredGameTemplatesToDownload = new List<string>();
        foreach (string templateId in availableGameTemplateIdList)
        {
            if (!GameTemplateLoader.IsGameTemplateDataFileStored(templateId))
            {
                requiredGameTemplatesToDownload.Add(templateId);
            }
        }
        return requiredGameTemplatesToDownload;
    }

    public static void GetGameTemplate(string templateId, DownloadSession downloadSession)
    {
        downloadSession.IncreaseTotalOperations(1);
        Debug.Log(
            $"Attempting to download game template {templateId} from {downloadSession.ServerIP} at port {downloadSession.ServerPort}");
        ContentServerAPIManager.Instance.GetGameTemplateData(
            downloadSession.ServerIP,
            downloadSession.ServerPort,
            templateId,
            (gameTemplate) => onGetGameTemplate(gameTemplate, downloadSession),
            (error) => downloadSession.ReportError(error, null));
    }

    private static void onGetGameTemplate(GameTemplate gameTemplate, DownloadSession downloadSession)
    {
        try
        {
            GameTemplateSaver.SaveGameTemplate(gameTemplate);
        }
        catch (Exception e)
        {
            downloadSession.ReportError(
                $"Failed to save game template {gameTemplate.Id}: {e.Message}",
                gameTemplate.Id);
            return;
        }
        List<string> requiredImagesToDownload = getRequiredImagesToDownloadList(gameTemplate);
        foreach (string imageFilename in requiredImagesToDownload)
        {
            GetImage(imageFilename, gameTemplate.Id, downloadSession);
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

    public static void GetImage(string imageFilename, string templateId, DownloadSession downloadSession)
    {
        downloadSession.IncreaseTotalOperations(1);
        Debug.Log(
            $"Attempting to download image {imageFilename} of game template {templateId} from {downloadSession.ServerIP} at port {downloadSession.ServerPort}");
        ContentServerAPIManager.Instance.GetImageTexture(
            downloadSession.ServerIP,
            downloadSession.ServerPort,
            imageFilename,
            templateId,
            (texture) => onGetImage(texture, imageFilename, templateId, downloadSession),
            (error) => downloadSession.ReportError(error, templateId));
    }

    private static void onGetImage(Texture2D texture, string imageFilename, string templateId, DownloadSession downloadSession)
    {
        SimulatorImageSaver.SaveImageWithFilename(
            texture,
            imageFilename,
            templateId,
            (_) => downloadSession.CompleteOneOperation(),
            (e) => downloadSession.ReportError(e.Message, templateId));
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

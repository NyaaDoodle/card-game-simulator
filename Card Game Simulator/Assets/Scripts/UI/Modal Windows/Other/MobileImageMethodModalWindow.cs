using System;
using UnityEngine;
using UnityEngine.UI;

public class MobileImageMethodModalWindow : ModalWindowBase
{
    [SerializeField] private Button browseFilesButton;
    [SerializeField] private Button useCameraButton;
    [SerializeField] private Button backButton;

    public void Setup(Action<Texture2D> onImageLoaded, Action onCancel, Action onBackButtonSelect)
    {
        setupBrowserFilesButton(onImageLoaded, onCancel);
        setupUseCameraButton(onImageLoaded, onCancel);
        setupBackButton(onBackButtonSelect);
    }

    private void setupBrowserFilesButton(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        browseFilesButton.onClick.RemoveAllListeners();
        browseFilesButton.onClick.AddListener(() => showNativeImageFilePicker(onImageLoaded, onCancel));
    }

    private void setupUseCameraButton(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        useCameraButton.onClick.RemoveAllListeners();
        useCameraButton.onClick.AddListener(() => showNativeCamera(onImageLoaded, onCancel));
    }

    private void setupBackButton(Action onBackButtonSelected)
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => onBackButtonSelected?.Invoke());
    }

    private void OnDestroy()
    {
        unsetBrowserFilesButton();
        unsetUseCameraButton();
        unsetBackButton();
    }

    private void unsetBrowserFilesButton()
    {
        browseFilesButton.onClick.RemoveAllListeners();
    }

    private void unsetUseCameraButton()
    {
        useCameraButton.onClick.RemoveAllListeners();
    }

    private void unsetBackButton()
    {
        backButton.onClick.RemoveAllListeners();
    }
    
    private void showNativeImageFilePicker(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        bool havePermissions = NativeFilePicker.CheckPermission();
        Debug.Log($"Have storage permissions? {havePermissions}");
        if (!havePermissions)
        {
            NativeFilePicker.RequestPermissionAsync((callback) =>
                {
                    Debug.Log($"Permission: {callback.ToString()}");
                    if (callback == NativeFilePicker.Permission.Denied)
                    {
                        Debug.Log("Permissions denied");
                        NativeFilePicker.OpenSettings();
                    }
                    else if (callback == NativeFilePicker.Permission.Granted)
                    {
                        Debug.Log("Permissions granted");
                        mobilePickAndLoadImageFile(onImageLoaded, onCancel);
                    }
                });
        }
        else
        {
            mobilePickAndLoadImageFile(onImageLoaded, onCancel);
        }
    }

    private void showNativeCamera(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        bool havePermissions = NativeCamera.CheckPermission(isPicturePermission: true);
        Debug.Log($"Have camera permissions? {havePermissions}");
        if (!havePermissions)
        {
            NativeCamera.RequestPermissionAsync((callback) =>
                {
                    Debug.Log($"Permission: {callback.ToString()}");
                    if (callback == NativeCamera.Permission.Denied)
                    {
                        Debug.Log("Permissions denied");
                        NativeCamera.OpenSettings();
                    }
                    else if (callback == NativeCamera.Permission.Granted)
                    {
                        Debug.Log("Permissions granted");
                        mobileTakePicture(onImageLoaded, onCancel);
                    }
                }, isPicturePermission: true);
        }
        else
        {
            mobileTakePicture(onImageLoaded, onCancel);
        }
    }
    
    private void mobilePickAndLoadImageFile(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        NativeFilePicker.PickFile(
            (path) =>
                {
                    if (string.IsNullOrEmpty(path)) onCancel?.Invoke();
                    SimulatorImageLoader.LoadTextureAbsolutePath(path, onImageLoaded, Debug.LogException);
                },
            new string[]
                {
                    NativeFilePicker.ConvertExtensionToFileType("jpg"),
                    NativeFilePicker.ConvertExtensionToFileType("png")
                });
    }

    private void mobileTakePicture(Action<Texture2D> onImageLoaded, Action onCancel)
    {
        NativeCamera.TakePicture((path) =>
            {
                if (string.IsNullOrEmpty(path)) onCancel?.Invoke();
                Texture2D texture = NativeCamera.LoadImageAtPath(path, markTextureNonReadable: false);
                if (texture != null)
                {
                    onImageLoaded?.Invoke(texture);
                }
            });
    }
}
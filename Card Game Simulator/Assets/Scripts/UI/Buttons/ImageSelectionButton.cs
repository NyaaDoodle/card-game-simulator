using System;
using System.Collections;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelectionButton : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform overlayObject;
    [SerializeField] private Sprite defaultSprite;
    private const string noImagePath = "";

    private Action<Texture2D> onImageSelected;
    private Action<Exception> onFailedImageLoad;

    private void Awake()
    {
        button.onClick.AddListener(onButtonSelect);
        setDefaultImage();
    }
    
    public void Show(string pathToImage, Action<Texture2D> onImageSelectedAction, Action<Exception> onFailedImageLoadAction)
    {
        gameObject.SetActive(true);
        this.onImageSelected = onImageSelectedAction;
        this.onFailedImageLoad = onFailedImageLoadAction;
        tryInitialLoadImage(pathToImage);
    }

    public void Hide()
    {
        setDefaultImage();
        gameObject.SetActive(false);
    }

    private void setDefaultImage()
    {
        displayImage.sprite = defaultSprite;
    }

    private void tryInitialLoadImage(string pathToImage)
    {
        if (pathToImage == noImagePath)
        {
            displayOverlay();
        }
        else
        {
            SimulatorImageLoader.LoadSpriteLocalPath(pathToImage,
                (sprite) =>
                    {
                        displayImage.sprite = sprite;
                        hideOverlay();
                    },
                (e) =>
                    {
                        onFailedImageLoad(e);
                        displayOverlay();
                    });
        }
    }

    private void displayOverlay()
    {
        overlayObject.gameObject.SetActive(true);
    }

    private void hideOverlay()
    {
        overlayObject.gameObject.SetActive(false);
    }

    private void onButtonSelect()
    {
        #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        ModalWindowManager.OpenMobileImageMethodModalWindow(
            (texture) =>
                {
                    ModalWindowManager.CloseCurrentWindow();
                    onTextureLoaded(texture);
                },
            () =>
                {
                    ModalWindowManager.CloseCurrentWindow();
                    Debug.Log("Mobile image selection action failed/cancelled");
                },
            ModalWindowManager.CloseCurrentWindow);
        #else
        showImagesFileBrowser();
        #endif
    }

    private void loadSelectedImage(string path)
    {
        if (path == null)
        {
            Debug.Log("Selecting image path failed/cancelled");
        }
        else
        {
            SimulatorImageLoader.LoadSpriteAbsolutePath(
                path,
                onSpriteLoaded,
                (e) =>
                    {
                        onFailedImageLoad(e);
                    });
        }
    }

    private void showImagesFileBrowser()
    {
        FileBrowser.SetFilters( false, new FileBrowser.Filter( "Images", ".jpg", ".png" ));
        FileBrowser.SetDefaultFilter( ".jpg" );
        StartCoroutine( showLoadDialogCoroutine() );
    }
    
    private IEnumerator showLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.Files, false, null, null, "Select Image", "Load" );
        if (FileBrowser.Success)
        {
            string imagePath = FileBrowser.Result[0];
            loadSelectedImage(imagePath);
        }
		else {
			Debug.Log("Failed to get filenames from load dialog");
            Debug.Log($"Have permissions? {FileBrowser.CheckPermission().ToString()}");
        }
    }

    private void onSpriteLoaded(Sprite sprite)
    {
        displayImage.sprite = sprite;
        hideOverlay();
        onImageSelected(sprite.texture);
    }

    private void onTextureLoaded(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, 
            new Rect(0, 0, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f));
        onSpriteLoaded(sprite);
    }
}
    

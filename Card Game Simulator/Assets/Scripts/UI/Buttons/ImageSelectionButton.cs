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
        tryLoadImage(pathToImage);
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

    private void tryLoadImage(string pathToImage)
    {
        if (pathToImage == noImagePath)
        {
            displayOverlay();
        }
        else
        {
            SimulatorImageLoader.LoadImage(pathToImage,
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
        showImagesFileBrowser();
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
            SimulatorImageLoader.LoadImage(imagePath,
                onTextureLoaded,
                (e) =>
                    {
                        onFailedImageLoad(e);
                    });
        }
    }

    private void onTextureLoaded(Sprite sprite)
    {
        displayImage.sprite = sprite;
        hideOverlay();
        onImageSelected(sprite.texture);
    }
}
    

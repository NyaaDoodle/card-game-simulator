using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTableSettingsScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField widthInputField;
    [SerializeField] private TMP_InputField heightInputField;
    [SerializeField] private ImageSelectionButton surfaceImageSelectionButton;
    
    public void Show()
    {
        gameObject.SetActive(true);
        SetupBaseButtons(GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen);
        setupInputFields();
        setupSurfaceImageSelectionButton();
    }

    public override void Hide()
    {
        unsetInputFields();
        unsetSurfaceImageSelectionButton();
        base.Hide();
    }

    private void setupInputFields()
    {
        setupInputFieldsText();
        setupInputFieldsEvents();
    }

    private void setupInputFieldsText()
    {
        TableData tableData = WorkingGameTemplate.TableData;
        widthInputField.text = tableData.Width.ToString();
        heightInputField.text = tableData.Height.ToString();
    }

    private void setupInputFieldsEvents()
    {
        widthInputField.onEndEdit.AddListener((text) =>
            {
                Image inputFieldImage = widthInputField.GetComponent<Image>();
                bool success = float.TryParse(text, out float width);
                if (success && width >= 0)
                {
                    inputFieldImage.color = Color.white;
                    WorkingGameTemplate.SetTableWidth(width);
                }
                else
                {
                    inputFieldImage.color = Color.red;
                }
            });
        heightInputField.onEndEdit.AddListener((text) =>
            {
                Image inputFieldImage = heightInputField.GetComponent<Image>();
                bool success = float.TryParse(text, out float height);
                if (success && height >= 0)
                {
                    inputFieldImage.color = Color.white;
                    WorkingGameTemplate.SetTableHeight(height);
                }
                else
                {
                    inputFieldImage.color = Color.red;
                }
            });
    }

    private void unsetInputFields()
    {
        widthInputField.onEndEdit.RemoveAllListeners();
        heightInputField.onEndEdit.RemoveAllListeners();
    }

    private void setupSurfaceImageSelectionButton()
    {
        string surfaceImagePath = WorkingGameTemplate.TableData.SurfaceImagePath;
        surfaceImageSelectionButton.Show(
            surfaceImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveImage(
                        texture,
                        WorkingGameTemplate.Id,
                        WorkingGameTemplate.SetTableSurfaceImage,
                        Debug.LogException);
                },
            (e) =>
                {
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void unsetSurfaceImageSelectionButton()
    {
        surfaceImageSelectionButton.Hide();
    }
}
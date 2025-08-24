using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTableSettingsScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField widthInputField;
    [SerializeField] private TMP_InputField heightInputField;
    [SerializeField] private ImageSelectionButton surfaceImageSelectionButton;
    
    public void Show(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(workingGameTemplate, () => goToGameTemplateSectionsScreen(workingGameTemplate));
        setupInputFields(workingGameTemplate);
        setupSurfaceImageSelectionButton(workingGameTemplate);
    }

    private void hide()
    {
        UnsetBaseButtons();
        unsetInputFields();
        unsetSurfaceImageSelectionButton();
        gameObject.SetActive(false);
    }
    
    private void goToGameTemplateSectionsScreen(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show(workingGameTemplate);
    }

    private void setupInputFields(WorkingGameTemplate workingGameTemplate)
    {
        setupInputFieldsText(workingGameTemplate);
        setupInputFieldsEvents(workingGameTemplate);
    }

    private void setupInputFieldsText(WorkingGameTemplate workingGameTemplate)
    {
        TableData tableData = workingGameTemplate.TableData;
        widthInputField.text = tableData.Width.ToString();
        heightInputField.text = tableData.Height.ToString();
    }

    private void setupInputFieldsEvents(WorkingGameTemplate workingGameTemplate)
    {
        widthInputField.onEndEdit.AddListener((text) =>
            {
                Image inputFieldImage = widthInputField.GetComponent<Image>();
                bool success = float.TryParse(text, out float width);
                if (success && width >= 0)
                {
                    inputFieldImage.color = Color.white;
                    workingGameTemplate.SetTableWidth(width);
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
                    workingGameTemplate.SetTableHeight(height);
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

    private void setupSurfaceImageSelectionButton(WorkingGameTemplate workingGameTemplate)
    {
        string surfaceImagePath = workingGameTemplate.TableData.SurfaceImagePath;
        surfaceImageSelectionButton.Show(
            surfaceImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveImage(
                        texture,
                        workingGameTemplate.Id,
                        workingGameTemplate.SetTableSurfaceImage,
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
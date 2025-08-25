using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditGameTemplateDetailsScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField templateNameInputField;
    [SerializeField] private TMP_InputField creatorNameInputField;
    [SerializeField] private TMP_InputField descriptionInputField;
    [SerializeField] private ImageSelectionButton templateImageSelectionButton;
    [SerializeField] private Button deleteTemplateButton;
    
    public void Show()
    {
        gameObject.SetActive(true);
        SetupBaseButtons(GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen);
        setInputFields();
        setTemplateImageSelectionButton();
        setDeleteTemplateButton();
    }

    public override void Hide()
    {
        unsetInputFieldsEvents();
        unsetTemplateImageSelectionButton();
        unsetDeleteTemplateButton();
        base.Hide();
    }

    private void setInputFields()
    {
        setInputFieldsText();
        setInputFieldsEvents();
    }

    private void setInputFieldsText()
    {
        GameTemplateDetails gameTemplateDetails = WorkingGameTemplate.GameTemplateDetails;
        templateNameInputField.text = gameTemplateDetails.TemplateName;
        creatorNameInputField.text = gameTemplateDetails.CreatorName;
        descriptionInputField.text = gameTemplateDetails.Description;
    }

    private void setInputFieldsEvents()
    {
        templateNameInputField.onEndEdit.AddListener(WorkingGameTemplate.SetTemplateName);
        creatorNameInputField.onEndEdit.AddListener(WorkingGameTemplate.SetTemplateCreatorName);
        descriptionInputField.onEndEdit.AddListener(WorkingGameTemplate.SetTemplateDescription);
    }

    private void unsetInputFieldsEvents()
    {
        templateNameInputField.onEndEdit.RemoveAllListeners();
        creatorNameInputField.onEndEdit.RemoveAllListeners();
        descriptionInputField.onEndEdit.RemoveAllListeners();
    }

    private void setTemplateImageSelectionButton()
    {
        string templateImagePath = WorkingGameTemplate.GameTemplateDetails.TemplateImagePath;
        templateImageSelectionButton.Show(
            templateImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveThumbnail(texture, WorkingGameTemplate.Id,
                        WorkingGameTemplate.SetTemplateThumbnail,
                        Debug.LogException);
                },
            (e) =>
                {
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void setDeleteTemplateButton()
    {
        deleteTemplateButton.onClick.AddListener(() =>
            {
                GameTemplateLoader.DeleteGameTemplate(WorkingGameTemplate.Id);
                GameTemplateEditor.Instance.GoToInitialScreen();
            });
    }

    private void unsetTemplateImageSelectionButton()
    {
        templateImageSelectionButton.Hide();
    }

    private void unsetDeleteTemplateButton()
    {
        deleteTemplateButton.onClick.RemoveAllListeners();
    }
} 
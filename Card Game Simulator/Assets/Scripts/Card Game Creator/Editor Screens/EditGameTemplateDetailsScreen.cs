using System;
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
    
    public void Show(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(workingGameTemplate, () => goToGameTemplateSectionsScreen(workingGameTemplate));
        setInputFields(workingGameTemplate);
        setTemplateImageSelectionButton(workingGameTemplate);
        setDeleteTemplateButton(workingGameTemplate);
    }

    private void hide()
    {
        UnsetBaseButtons();
        unsetInputFieldsEvents();
        unsetTemplateImageSelectionButton();
        unsetDeleteTemplateButton();
        gameObject.SetActive(false);
    }

    private void setInputFields(WorkingGameTemplate workingGameTemplate)
    {
        setInputFieldsText(workingGameTemplate);
        setInputFieldsEvents(workingGameTemplate);
    }

    private void setInputFieldsText(WorkingGameTemplate workingGameTemplate)
    {
        GameTemplateDetails gameTemplateDetails = workingGameTemplate.GameTemplateDetails;
        templateNameInputField.text = gameTemplateDetails.TemplateName;
        creatorNameInputField.text = gameTemplateDetails.CreatorName;
        descriptionInputField.text = gameTemplateDetails.Description;
    }

    private void setInputFieldsEvents(WorkingGameTemplate workingGameTemplate)
    {
        templateNameInputField.onEndEdit.AddListener(workingGameTemplate.SetTemplateName);
        creatorNameInputField.onEndEdit.AddListener(workingGameTemplate.SetTemplateCreatorName);
        descriptionInputField.onEndEdit.AddListener(workingGameTemplate.SetTemplateDescription);
    }

    private void unsetInputFieldsEvents()
    {
        templateNameInputField.onEndEdit.RemoveAllListeners();
        creatorNameInputField.onEndEdit.RemoveAllListeners();
        descriptionInputField.onEndEdit.RemoveAllListeners();
    }

    private void setTemplateImageSelectionButton(WorkingGameTemplate workingGameTemplate)
    {
        string templateImagePath = workingGameTemplate.GameTemplateDetails.TemplateImagePath;
        templateImageSelectionButton.Show(
            templateImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveThumbnail(texture, workingGameTemplate.Id,
                        workingGameTemplate.SetTemplateThumbnail,
                        Debug.LogException);
                },
            (e) =>
                {
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void setDeleteTemplateButton(WorkingGameTemplate workingGameTemplate)
    {
        deleteTemplateButton.onClick.AddListener(() =>
            {
                GameTemplateLoader.DeleteGameTemplate(workingGameTemplate.Id);
                this.hide();
                GameTemplateEditorScreenReferences.Instance.GameTemplateSelectionScreen.Show();
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

    private void goToGameTemplateSectionsScreen(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show(workingGameTemplate);
    }
} 
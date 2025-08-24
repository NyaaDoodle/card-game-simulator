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
    
    public void Show(WorkingGameTemplate workingGameTemplate, Action onBackButtonSelect)
    {
        SetupBaseButtons(onBackButtonSelect);
        gameObject.SetActive(true);
        setInputFields(workingGameTemplate);
        setTemplateImageSelectionButton(workingGameTemplate);
        setDeleteTemplateButton(workingGameTemplate);
    }

    public void Hide()
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
                        (savedThumbnailLocalPath) =>
                            {
                                Debug.Log(savedThumbnailLocalPath);
                                workingGameTemplate.SetTemplateThumbnail(savedThumbnailLocalPath);
                            },
                        Debug.LogException);
                },
            (e) =>
                {
                    // PLACEHOLDER
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void setDeleteTemplateButton(WorkingGameTemplate workingGameTemplate)
    {
        // PLACEHOLDER
        deleteTemplateButton.onClick.AddListener(() => Debug.Log("Clicked delete template button!"));
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
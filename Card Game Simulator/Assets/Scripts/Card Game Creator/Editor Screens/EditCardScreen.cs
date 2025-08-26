using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCardScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private TMP_InputField cardNameInputField;
    [SerializeField] private TMP_InputField cardDescriptionInputField;
    [SerializeField] private ImageSelectionButton frontSideImageSelectionButton;
    [SerializeField] private ImageSelectionButton backSideImageSelectionButton;
    [SerializeField] private Button deleteCardButton;

    private string currentCardId;
    private CardData currentCardData => WorkingGameTemplate.CardPool[currentCardId];

    public void Show(CardData cardData)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen);
        currentCardId = cardData.Id;
        setInputFields();
        setImageSelectionButtons();
        setDeleteButton();
    }

    public override void Hide()
    {
        currentCardId = null;
        unsetInputFields();
        unsetImageSelectionButtons();
        unsetDeleteButton();
        base.Hide();
    }

    private void setInputFields()
    {
        setInputFieldsText();
        setInputFieldsEvents();
    }

    private void setInputFieldsText()
    {
        cardNameInputField.text = currentCardData.Name;
        cardDescriptionInputField.text = currentCardData.Description;
    }

    private void setInputFieldsEvents()
    {
        cardNameInputField.onEndEdit.AddListener((text) => WorkingGameTemplate.SetCardName(currentCardData, text));
        cardDescriptionInputField.onEndEdit.AddListener((text) =>
            WorkingGameTemplate.SetCardDescription(currentCardData, text));
    }

    private void setImageSelectionButtons()
    {
        setFrontSideImageSelectionButton();
        setBackSideImageSelectionButton();
    }

    private void setFrontSideImageSelectionButton()
    {
        frontSideImageSelectionButton.Show(currentCardData.FrontSideImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveImage(
                        texture,
                        WorkingGameTemplate.Id,
                        (path) =>
                            {
                                WorkingGameTemplate.SetCardFrontSideImagePath(currentCardData, path);
                            },
                        Debug.LogException);
                },
            (e) =>
                {
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void setBackSideImageSelectionButton()
    {
        backSideImageSelectionButton.Show(currentCardData.BackSideImagePath,
            (texture) =>
                {
                    SimulatorImageSaver.SaveImage(
                        texture,
                        WorkingGameTemplate.Id,
                        (path) =>
                            {
                                WorkingGameTemplate.SetCardBackSideImagePath(currentCardData, path);
                            },
                        Debug.LogException);
                },
            (e) =>
                {
                    Debug.Log("Failed to load image");
                    Debug.LogException(e);
                });
    }

    private void setDeleteButton()
    {
        deleteCardButton.onClick.AddListener(() =>
            {
                WorkingGameTemplate.DeleteCardData(currentCardData);
                GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
            });
    }
    
    private void unsetInputFields()
    {
        cardNameInputField.onEndEdit.RemoveAllListeners();
        cardDescriptionInputField.onEndEdit.RemoveAllListeners();
    }

    private void unsetImageSelectionButtons()
    {
        frontSideImageSelectionButton.Hide();
        backSideImageSelectionButton.Hide();
    }

    private void unsetDeleteButton()
    {
        deleteCardButton.onClick.RemoveAllListeners();
    }
}